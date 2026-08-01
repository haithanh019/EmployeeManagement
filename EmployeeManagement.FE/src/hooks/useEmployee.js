import { useState, useEffect, useCallback } from 'react';
import { message } from 'antd';
import { employeeApi } from '../api/employeeApi';

export function useEmployee() {
  const [employees, setEmployees] = useState([]);
  const [loading, setLoading] = useState(false);

  const fetchAll = useCallback(async () => {
    setLoading(true);
    try {
      const res = await employeeApi.getAll();
      setEmployees(res.data);
    } catch {
      message.error('Không thể tải danh sách nhân viên');
    } finally {
      setLoading(false);
    }
  }, []);

  useEffect(() => { fetchAll(); }, [fetchAll]);

  const create = async (data) => {
    await employeeApi.create(data);
    message.success('Thêm nhân viên thành công!');
    fetchAll();
  };

  const update = async (id, data) => {
    await employeeApi.update(id, data);
    message.success('Cập nhật thành công!');
    fetchAll();
  };

  const remove = async (id) => {
    await employeeApi.delete(id);
    message.success('Đã xoá nhân viên!');
    fetchAll();
  };

  return { employees, loading, create, update, remove, fetchAll };
}