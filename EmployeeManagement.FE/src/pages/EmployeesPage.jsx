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
    { title: 'Full Name', dataIndex: 'fullName',   key: 'fullName' },
    { title: 'Position',  dataIndex: 'position',   key: 'position' },
    {
      title: 'Hourly Rate', dataIndex: 'hourlyRate', key: 'hourlyRate',
      render: (v) => `${v.toLocaleString('en-US')} VND`,
    },
    {
      title: 'Status', dataIndex: 'isActive', key: 'isActive',
      render: (v) => <Tag color={v ? 'green' : 'red'}>{v ? 'Active' : 'Inactive'}</Tag>,
    },
    {
      title: 'Actions', key: 'action',
      render: (_, record) => (
        <Space>
          <Button icon={<EditOutlined />} onClick={() => openEdit(record)}>Edit</Button>
          <Popconfirm title="Confirm deletion?" onConfirm={() => remove(record.id)}>
            <Button danger icon={<DeleteOutlined />}>Delete</Button>
          </Popconfirm>
        </Space>
      ),
    },
  ];

  return (
    <div style={{ padding: 24 }}>
      <div style={{ display: 'flex', justifyContent: 'space-between', marginBottom: 16 }}>
        <h2>Employee Management</h2>
        <Button type="primary" icon={<PlusOutlined />} onClick={openCreate}>
          Add Employee
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