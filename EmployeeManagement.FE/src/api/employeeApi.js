import axiosInstance from './axiosInstance';

const BASE = '/employees';

export const employeeApi = {
  getAll:  ()         => axiosInstance.get(BASE),
  getById: (id)        => axiosInstance.get(`${BASE}/${id}`),
  create:  (data)      => axiosInstance.post(BASE, data),
  update:  (id, data)  => axiosInstance.put(`${BASE}/${id}`, data),
  delete:  (id)         => axiosInstance.delete(`${BASE}/${id}`),
};