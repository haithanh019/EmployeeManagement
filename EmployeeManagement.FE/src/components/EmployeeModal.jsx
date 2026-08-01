import { useEffect } from 'react';
import { Modal, Form, Input, InputNumber, Switch } from 'antd';

export default function EmployeeModal({ open, onClose, onSubmit, employee }) {
  const [form] = Form.useForm();

  useEffect(() => {
    if (open) {
      employee ? form.setFieldsValue(employee) : form.resetFields();
    }
  }, [open, employee, form]);

  const handleOk = async () => {
    const values = await form.validateFields();
    onSubmit(values);
  };

  return (
    <Modal
      title={employee ? 'Cập nhật nhân viên' : 'Thêm nhân viên mới'}
      open={open}
      onOk={handleOk}
      onCancel={onClose}
      okText={employee ? 'Cập nhật' : 'Thêm'}
      cancelText="Huỷ"
    >
      <Form form={form} layout="vertical">
        <Form.Item name="fullName" label="Họ tên"
          rules={[{ required: true, message: 'Vui lòng nhập họ tên' }]}>
          <Input />
        </Form.Item>
        <Form.Item name="position" label="Chức vụ"
          rules={[{ required: true, message: 'Vui lòng nhập chức vụ' }]}>
          <Input />
        </Form.Item>
        <Form.Item name="hourlyRate" label="Lương/giờ"
          rules={[{ required: true }]}>
          <InputNumber min={0} style={{ width: '100%' }}
            formatter={v => `${v}`.replace(/\B(?=(\d{3})+(?!\d))/g, ',')}
            addonAfter="VNĐ" />
        </Form.Item>
        {employee && (
          <Form.Item name="isActive" label="Đang làm việc" valuePropName="checked">
            <Switch />
          </Form.Item>
        )}
      </Form>
    </Modal>
  );
}