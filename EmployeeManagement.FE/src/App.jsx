import { Layout, Menu } from 'antd';
import { TeamOutlined, FileTextOutlined } from '@ant-design/icons';
import { BrowserRouter, Routes, Route, useNavigate, useLocation } from 'react-router-dom';
import EmployeesPage from './pages/EmployeesPage';
import SalaryPage from './pages/SalaryPage';

const { Sider, Content } = Layout;

function AppLayout() {
  const navigate = useNavigate();
  const location = useLocation();

  const menuItems = [
    { key: '/',       icon: <TeamOutlined />,     label: 'Nhân viên' },
    { key: '/salary', icon: <FileTextOutlined />, label: 'Báo cáo lương' },
  ];

  return (
    <Layout style={{ minHeight: '100vh' }}>
      <Sider>
        <div style={{ color: '#fff', textAlign: 'center', padding: '20px 0', fontWeight: 'bold' }}>
          Employee Manager
        </div>
        <Menu
          theme="dark"
          selectedKeys={[location.pathname]}
          items={menuItems}
          onClick={({ key }) => navigate(key)}
        />
      </Sider>
      <Layout>
        <Content>
          <Routes>
            <Route path="/"       element={<EmployeesPage />} />
            <Route path="/salary" element={<SalaryPage />} />
          </Routes>
        </Content>
      </Layout>
    </Layout>
  );
}

export default function App() {
  return (
    <BrowserRouter>
      <AppLayout />
    </BrowserRouter>
  );
}