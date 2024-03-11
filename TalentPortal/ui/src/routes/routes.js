import Urlerror from "../components/error/urlerror.svelte";
import Login from "../pages/auth/login/login.svelte";
import Profile from "../pages/auth/profile/profile.svelte";
import Attendance from "../pages/dashboard/attendance.svelte";
import Dashboard from "../pages/dashboard/dashboard.svelte";
import AddDsr from "../pages/dsr/adddsr.svelte";
import Receiveddsr from "../pages/dsr/receiveddsr.svelte";
import SentDsr from "../pages/dsr/sentdsr.svelte";
import ApplyLeave from "../pages/leaves/applyLeave.svelte";
import CancelLeave from "../pages/leaves/cancelLeave.svelte";
import MyLeaves from "../pages/leaves/myLeaves.svelte";
import CreateReport from "../pages/report/createReport.svelte";
import ReceivedReport from "../pages/report/receivedReport.svelte";
import Sentreport from "../pages/report/sentreport.svelte";


export const routes = [
    {
        path: '/dashboard',
        isPublic: false,
        name: 'Home',
        hasChildren: false,
        component: Dashboard,
    },
    {
        path: '/attendance',
        isPublic: false,
        name: 'Attendance',
        hasChildren: false,
        component: Attendance,
    },
    {
        path: '/dsr',
        isPublic: false,
        hasChildren: true,
        name: 'DSR',
        children: [
            {
                path: '/dsr/add',
                isPublic: false,
                hasChildren: false,
                name: 'DSR Create',
                component: AddDsr
            },
            {
                path: '/dsr/sent',
                isPublic: false,
                hasChildren: true,
                name: 'Sent DSR',
                component: SentDsr
            },
            {
                path: '/dsr/received',
                isPublic: false,
                hasChildren: true,
                name: 'Received DSR',
                component: Receiveddsr
            }
        ]
    },
    {
        path: '/report',
        isPublic: false,
        hasChildren: true,
        name: 'Report',
        children: [
            {
                path: '/report/add',
                isPublic: false,
                hasChildren: false,
                name: 'Create Report',
                component: CreateReport
            },
            {
                path: '/report/sent',
                isPublic: false,
                hasChildren: true,
                name: 'Sent Report',
                component: Sentreport
            },
            {
                path: '/report/received',
                isPublic: false,
                hasChildren: true,
                name: 'Received Report',
                component: ReceivedReport
            },

        ]
    },
    {
        path :'/leave',
        isPublic : false,
        hasChildren: true,
        name: 'Leaves',
        children: [
            {
                path: '/leave/create',
                isPublic: false,
                hasChildren: false,
                name: 'Apply Leave',
                component: ApplyLeave
            },
            {
                path: '/leave/cancel',
                isPublic: false,
                hasChildren: true,
                name: 'Cancel Leave',
                component: CancelLeave
            },
            {
                path: '/leave/requests',
                isPublic: false,
                hasChildren: true,
                name: 'My Requests',
                component: MyLeaves
            }
        ]
    },
    {
        path: '/auth/login',
        isPublic: true,
        name: 'Login',
        hasChildren: false,
        component: Login
    },
    {
        path: '/profile',
        isPublic: false,
        name: 'Profile',
        hasChildren: false,
        component: Profile,
    },
]