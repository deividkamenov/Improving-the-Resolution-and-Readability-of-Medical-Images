import React from 'react';
import { Empty } from 'antd';
import ReactImageMagnify from 'react-image-magnify';

interface IProps {
    hasInputImage: boolean;
    picture: any
}

export default ({ hasInputImage, picture }: IProps) => {
    return <>
        <div className="outputImage">
            {picture &&
                <ReactImageMagnify {...{
                    enlargedImagePosition: "over",
                    isHintEnabled: true,
                    smallImage: {
                        alt: 'Output image',
                        isFluidWidth: true,
                        src: picture
                    },
                    largeImage: {
                        src: picture,
                        width: 1200,
                        height: 1800
                    }
                }} />

                // <img alt="" src={picture} style={{ width: '100%' }} />
            }
            {!picture && <Empty description={hasInputImage ? "Please apply transformation..." : "Please add Input image..."} style={{ marginTop: '5rem' }} />}
        </div>
    </>
}