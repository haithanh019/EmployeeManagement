import { useState } from 'react';
import { Table, Button, Space, Tag, Popconfirm } from 'antd';
import { PlusOutlined, EditOutlined, DeleteOutlined } from '@ant-design/icons';
import { useEmployee } from '../hooks/useEmployee';
import EmployeeModal from '../components/EmployeeModal';

export default function EmployeesPage() {
  const { employees, loading, create, update, remove } = useEmployee();
  const [modalOpen, setModalOpen] = useState(false);
  const [selected, setSelected] = useState(null);

  const openCreate = () => { setSelected(null); setModalOpen(true); };
  const openEdit   = (emp) => { setSelected(emp); setModalOpen(true); };
  const closeModal = () => setModalOpen(false);

  const handleSubmit = async (values) => {
    if (selected) {
      await update(selected.id, { ...values, id: selected.id });
    } else {
      await create(values);
    }
    closeModal();
  };

  const columns = [
    { title: 'Họ tên',    dataIndex: 'fullName',   key: 'fullName' },
    { title: 'Chức vụ',   dataIndex: 'position',   key: 'position' },
    {
      title: 'Lương/giờ', dataIndex: 'hourlyRate', key: 'hourlyRate',
      render: (v) => `${v.toLocaleString('vi-VN')} VNĐ`,
    },
    {
      title: 'Trạng thái', dataIndex: 'isActive', key: 'isActive',
      render: (v) => <Tag color={v ? 'green' : 'red'}>{v ? 'Đang làm' : 'Nghỉ việc'}</Tag>,
    },
    {
      title: 'Thao tác', key: 'action',
      render: (_, record) => (
        <Space>
          <Button icon={<EditOutlined />} onClick={() => openEdit(record)}>Sửa</Button>
          <Popconfirm title="Xác nhận xoá?" onConfirm={() => remove(record.id)}>
            <Button danger icon={<DeleteOutlined />}>Xoá</Button>
          </Popconfirm>
        </Space>
      ),
    },
  ];

  return (
    <div style={{ padding: 24 }}>
      <div style={{ display: 'flex', justifyContent: 'space-between', marginBottom: 16 }}>
        <h2>Quản lý nhân viên</h2>
        <Button type="primary" icon={<PlusOutlined />} onClick={openCreate}>
          Thêm nhân viên
        </Button>
      </div>
      <Table
        rowKey="id"
        columns={columns}
        dataSource={employees}
        loading={loading}
      />
      <EmployeeModal
        open={modalOpen}
        onClose={closeModal}
        onSubmit={handleSubmit}
        employee={selected}
      />
    </div>
  );
}