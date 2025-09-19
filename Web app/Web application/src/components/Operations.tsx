import React, { useState } from 'react';
import { Button, Switch } from 'antd';
import { OPERATIONS } from '../OPERATIONS';
import { ArrowsAltOutlined, ShrinkOutlined, BuildOutlined, ReadOutlined, SaveOutlined, RiseOutlined } from '@ant-design/icons';
interface IProps {
    canSave: boolean;
    onOperation: (operation: OPERATIONS) => void;
}

export default ({ canSave, onOperation }: IProps) => {
    const [edgePreserving, setEdgePreserving] = useState<boolean>(false);
    const [parallel, setParallel] = useState<boolean>(false);
    const onEdgePreserving = (edgePreserving: boolean) => {
        setEdgePreserving(edgePreserving)
    }
    const onParalel = (parallel: boolean) => {
        setParallel(parallel)
    }
    return <>
        <div className="edgePreserving">
            <span><BuildOutlined />Edge Preserving</span>
            <Switch checked={edgePreserving} onChange={onEdgePreserving} />
        </div>
        <div className="edgePreserving">
            <span><RiseOutlined />Parallel processing</span>
            <Switch checked={parallel} onChange={onParalel} />
        </div>
        <Button onClick={() => {
            onOperation(OPERATIONS.NOP)
        }} size="large">No Transformation</Button>
        <Button onClick={() => {
            onOperation(edgePreserving ? OPERATIONS.UPSAMPLE_EDGE : OPERATIONS.UPSAMPLE_STANDART)
        }} size="large"><ArrowsAltOutlined />Upsample</Button>
        <Button onClick={() => {
            onOperation(edgePreserving ? OPERATIONS.DOWNSAMPLE_EDGE : OPERATIONS.DOWNSAMPLE_STANDART)
        }} size="large"><ShrinkOutlined />Downsample</Button>
        <Button onClick={() => {
            onOperation(edgePreserving ? OPERATIONS.READABILITY_EDGE : OPERATIONS.READABILITY_STANDART)
        }} size="large"><ReadOutlined /> Improve Readability</Button>

        <Button style={{ marginTop: '3rem' }} disabled={!canSave} onClick={() => {
            onOperation(OPERATIONS.SAVE_IMAGE)
        }} size="large"><SaveOutlined />Save As...</Button>
    </>
}