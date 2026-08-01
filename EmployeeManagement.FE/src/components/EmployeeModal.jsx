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
      title={employee ? 'Update Employee' : 'Add New Employee'}
      open={open}
      onOk={handleOk}
      onCancel={onClose}
      okText={employee ? 'Update' : 'Add'}
      cancelText="Cancel"
    >
      <Form form={form} layout="vertical">
        <Form.Item name="fullName" label="Full Name"
          rules={[{ required: true, message: 'Please enter full name' }]}>
          <Input />
        </Form.Item>
        <Form.Item name="position" label="Position"
          rules={[{ required: true, message: 'Please enter position' }]}>
          <Input />
        </Form.Item>
        <Form.Item name="hourlyRate" label="Hourly Rate"
          rules={[{ required: true }]}>
          <InputNumber min={0} style={{ width: '100%' }}
            formatter={v => `${v}`.replace(/\B(?=(\d{3})+(?!\d))/g, ',')}
            addonAfter="VND" />
        </Form.Item>
        {employee && (
          <Form.Item name="isActive" label="Active" valuePropName="checked">
            <Switch />
          </Form.Item>
        )}
      </Form>
    </Modal>
  );
}