import axiosInstance from './axiosInstance';

export const salaryApi = {
  exportExcel: (fromDate, toDate) =>
    axiosInstance.get('/salaries/export', {
      params: { fromDate, toDate },
      responseType: 'blob',
    }),
};