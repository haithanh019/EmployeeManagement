import { useState } from 'react';
import { DatePicker, Button, Card, message } from 'antd';
import { DownloadOutlined } from '@ant-design/icons';
import dayjs from 'dayjs';
import { salaryApi } from '../api/salaryApi';

const { RangePicker } = DatePicker;

export default function SalaryPage() {
  const [dates, setDates] = useState(null);
  const [loading, setLoading] = useState(false);

  const handleExport = async () => {
    if (!dates) return message.warning('Vui lòng chọn khoảng thời gian');
    setLoading(true);
    try {
      const from = dates[0].format('YYYY-MM-DD');
      const to   = dates[1].format('YYYY-MM-DD');
      const res  = await salaryApi.exportExcel(from, to);

      const url  = window.URL.createObjectURL(new Blob([res.data]));
      const link = document.createElement('a');
      link.href  = url;
      link.setAttribute('download', `SalaryReport_${from}_${to}.xlsx`);
      document.body.appendChild(link);
      link.click();
      link.remove();
    } catch {
      message.error('Xuất báo cáo thất bại');
    } finally {
      setLoading(false);
    }
  };

  return (
    <div style={{ padding: 24 }}>
      <h2>Báo cáo lương</h2>
      <Card style={{ maxWidth: 500, marginTop: 16 }}>
        <p style={{ marginBottom: 12 }}>Chọn khoảng thời gian:</p>
        <RangePicker
          style={{ width: '100%', marginBottom: 16 }}
          onChange={setDates}
          defaultValue={[dayjs().startOf('month'), dayjs()]}
        />
        <Button
          type="primary"
          icon={<DownloadOutlined />}
          loading={loading}
          onClick={handleExport}
          block
        >
          Xuất Excel
        </Button>
      </Card>
    </div>
  );
}