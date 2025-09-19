import React, { useState } from 'react';
import { Row, Col } from 'antd'
import InputImage from './components/InputImage';

import { Layout } from 'antd';
import { OPERATIONS } from './OPERATIONS';
import Operations from './components/Operations';
import OutputImage from './components/OutputImage';
import { saveAs } from 'file-saver';

import 'antd/dist/antd.css';
import './App.css';

const { Header, Content } = Layout;

export default () => {
  const [inputPicture, setInputPicture] = useState<any>();
  const [outputPicture, setOutputPicture] = useState<any>();

  const onOperation = (op: OPERATIONS) => {
    console.log("onOperation")
    if (op === OPERATIONS.SAVE_IMAGE) {
      saveAs(outputPicture, "output.jpg");
    } else {
      setOutputPicture(inputPicture)
    }

  }
  return (
    <Layout>
      <Header className="header">
        <span>Повишаване на резолюцията и четимостта на двумерни медицински изображения посредством математически трансформации</span>
      </Header>
      <Content className="content">
        <Row justify="space-around" align="top">
          <Col span={10} className="inputImageBox">
            <h1>Input image</h1>
            <InputImage setPicture={(picture) => {
              setInputPicture(picture)
              setOutputPicture(null)
            }} />
          </Col>
          <Col span={4} className="controlsBox">
            <Operations canSave={!!outputPicture} onOperation={onOperation} />
          </Col>
          <Col span={10} className="outputImageBox">
            <h1>Output image</h1>
            <OutputImage hasInputImage={!!inputPicture} picture={outputPicture} />
          </Col>
        </Row>
      </Content>
    </Layout>
  );
}
