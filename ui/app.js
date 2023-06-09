const routes=[
    {path:'/home',component:home},
    {path:'/employee',component:employee},
    {path:'/department',component:department}
]

var router = new VueRouter({
    routes:routes
})

const app = new Vue({
    router
}).$mount('#app')