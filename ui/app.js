const routes=[
    {path:'/home',component:home},
    {path:'/employee',component:employee},
    {path:'/student',component:student}
]

var router = new VueRouter({
    routes:routes
})

const app = new Vue({
    router
}).$mount('#app')