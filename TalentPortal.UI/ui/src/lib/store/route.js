export const routes = [
    { name: 'Home', path: '/', component: 'Home.svelte', type: 'public' },
    { name: 'Login', path: '/login', component: 'Login.svelte' , type: 'public'},
    { name: 'DSR', path: '/dsr', component: 'DSR.svelte' , type: 'private'},
    { name: 'DSR Create', path: '/dsr/add', component: 'DSRCreate.svelte' , type: 'private'},
]