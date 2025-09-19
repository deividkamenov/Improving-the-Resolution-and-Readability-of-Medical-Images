import * as fs from 'fs';
import * as tf from '@tensorflow/tfjs'
import * as tfnode from '@tensorflow/tfjs-node'
import * as mobilenet from '@tensorflow-models/mobilenet'
import * as knnClassifier from '@tensorflow-models/knn-classifier'
const ImageJS = require('imagejs');
const express = require('express')

// Loading dataset
const images: Array<{ finding: string, filename: string }> = require('./dataset2.js');

const app = express()
const port = 3456

process.on('unhandledRejection', error => {
    // Will print "unhandledRejection err is not defined"
    console.log('unhandledRejection', error);
});

const readImage = path => {
    const imageBuffer = fs.readFileSync(path);
    const tfimage = tfnode.node.decodeImage(imageBuffer);
    return tfimage as tf.Tensor3D;
}

console.log(images);

const bodyParser = require('body-parser');
(
    async () => {
        const net = await mobilenet.load();

        app.use(bodyParser.json({ limit: "50mb" }));

        // Training dataset
        const classifier = knnClassifier.create();

        // Loading training set
        let totalImages = 0;
        images.forEach((data) => {
            try {
                if (totalImages > 45) {
                    return;
                }
                const img = readImage('./images/images/' + data.filename);
                const activation = net.infer(img, true);
                classifier.addExample(activation, data.finding);
                totalImages += 1;

                console.log(totalImages);
            } catch (e) {
                console.log(`Skipping ${data.filename}`);
            }
        });

        app.post('/covid', (req, res) => {
            const imageHeight = req.body.Height;
            const imageWidth = req.body.Width;
            if (req.body.imageR) {
                console.log("There is image. Building...");
                const imgR = Buffer.alloc(imageHeight * imageWidth, req.body.imageR, 'base64');
                const imgG = Buffer.alloc(imageHeight * imageWidth, req.body.imageG, 'base64');
                const imgB = Buffer.alloc(imageHeight * imageWidth, req.body.imageB, 'base64');
                console.log("Buffers generated");

                const myImage = new ImageJS.Bitmap({
                    width: imageWidth,
                    height: imageHeight
                });

                let col = 0;
                let row = 0;
                let i = 0;

                imgR.forEach(((pixel, i) => {
                    if (i % imageWidth === 0) {
                        col = 0;
                        row += 1;
                    }

                    i += 1;
                    console.log(i, imgR.length);
                    myImage.setPixel(row, col, imgR, imgG[i], imgB[i], 0);
                    col += 1
                }));
                console.log('Image build success. Saving...');
                console.log({ imageWidth, imageHeight })
                return myImage.writeFile("covid2.jpg").then(async () => {
                    console.log("Image saved!. Classifing..")
                    const activation = net.infer(readImage('./covid2.jpg'), true);
                    const result = await classifier.predictClass(activation);

                    console.log('Image classified sending response');
                    console.log(result);
                    res.send(Object.keys(result.confidences).map((key) => {
                        return `${key} - ${(result.confidences[key] * 100).toFixed(2)}% `
                    }).join("\n"))
                });
            }
        })
        app.post('/general', (req, res) => {
            const imageHeight = req.body.Height;
            const imageWidth = req.body.Width;
            if (req.body.imageR) {
                console.log("There is image. Building...");
                const imgR = Buffer.alloc(imageHeight * imageWidth, req.body.imageR, 'base64');
                const imgG = Buffer.alloc(imageHeight * imageWidth, req.body.imageG, 'base64');
                const imgB = Buffer.alloc(imageHeight * imageWidth, req.body.imageB, 'base64');
                console.log("Buffers generated");

                const myImage = new ImageJS.Bitmap({
                    width: imageHeight,
                    height: imageWidth
                });

                let col = 0;
                let row = 0;
                let i = 0;



                imgR.forEach(((pixel, i) => {
                    if (i % imageWidth === 0) {
                        col = 0;
                        row += 1;
                    }

                    i += 1;
                    console.log(i, imgR.length);
                    myImage.setPixel(row, col, imgR, imgG[i], imgB[i], 255);
                    col += 1
                }));
                console.log('Image build success. Saving...');
                console.log({ imageWidth, imageHeight })
                return myImage.writeFile("general.jpg").then(async () => {
                    console.log("Image saved!. Classifing..")
                    const result = await net.classify(readImage('./general.jpg'));
                    console.log('Image classified sending response');

                    if (result && result.length) {
                        console.log(result);
                        res.send(result.map((item) => {
                            return `${item.className} - ${(item.probability * 100).toFixed(2)}% `
                        }).join("\n"))
                    } else {
                        res.send("The image could not be recognized")
                    }
                });
            }

        })

        app.listen(port, () => console.log(`Example app listening at http://localhost:${port}`))

    }
)()
