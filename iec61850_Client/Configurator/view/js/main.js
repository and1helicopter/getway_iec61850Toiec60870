new Vue({
    el: "#app",
    data: {
        servers:[],
        items:[],
        text: "Test1",
        show: false
    },
    methods:{
        click: function () {
            this.show = !this.show
        }
    }
})