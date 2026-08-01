import axiosInstance from './axiosInstance';

export const workLogApi = {
  create: (data) => axiosInstance.post('/worklogs', data),
};