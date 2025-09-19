import React, { useState } from 'react';
import { Upload, message, Button } from 'antd';

import { LoadingOutlined } from '@ant-design/icons';
import { UploadOutlined } from '@ant-design/icons';

function getBase64(img: any, callback: any) {
    const reader = new FileReader();
    reader.addEventListener('load', () => callback(reader.result));
    reader.readAsDataURL(img);
}

function beforeUpload(file: any) {
    const isJpgOrPng = file.type === 'image/jpeg' || file.type === 'image/png' || file.type === 'image/tiff';
    if (!isJpgOrPng) {
        message.error('You can only upload JPG/PNG file!');
    }
    const isLt5M = file.size / 1024 / 1024 < 5;
    if (!isLt5M) {
        message.error('Image must smaller than 5MB!');
    }
    return isJpgOrPng && isLt5M;
}

interface IProps {
    setPicture: (picture: any) => void;
}

export default ({ setPicture }: IProps) => {
    const [loading, setLoading] = useState(false);
    const [image, setImage] = useState()
    const uploadButton = (
        <Button size="large" style={{ marginTop: '5rem' }}>
            {loading ? <LoadingOutlined /> : <div><UploadOutlined /> Add Input image</div>}
        </Button>
    );
    const handleChange = (info: any) => {
        if (info.file.status === 'uploading') {
            setLoading(true);
            return;
        }
        if (info.file.status === 'done') {
            // Get this url from response in real world.
            getBase64(info.file.originFileObj, (imageUrl: any) => {
                setLoading(false);
                setPicture(imageUrl)
                setImage(imageUrl)
                console.log(imageUrl);
            });
        }
    };
    return <Upload
        style={{ width: '100%', minHeight: '400px' }}
        name="avatar"
        listType="picture"
        showUploadList={false}
        action="https://www.mocky.io/v2/5cc8019d300000980a055e76"
        beforeUpload={beforeUpload}
        onChange={handleChange}
    >
        {image ? <img src={image} alt="avatar" style={{ width: '100%' }} /> : uploadButton}
    </Upload>
}