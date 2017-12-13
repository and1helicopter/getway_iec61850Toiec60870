var data = {
    host:'localhost',
    port: 2404,
    maxQueue: 10
};

Vue.component('app-iec60870', {
    template: '#iec60870',
    data: function () {
        return data
    },
    methods: {
        changeHost: function () {
            console.log(data.host)
        },
        changePort: function () {
            console.log(data.port)
        }
    }
});

Vue.component('app-header_right', {
    template: '#headerRight',
    props: ["show"],
    data: function () {
        return data
    },
    methods: {
        show_on: function () {
            this.show = !this.show;
            this.$emit('showstatuschange', this.show);
            console.log(this.show);
        }
    }
});

new Vue({
    el: '#app',
    data:{
        show60870: false,
        show61850: false
    },
    methods:{
        change_show60870: function (value) {
            this.show60870 = value;
            if(this.show60870){
                this.show61850 = false
            }
            console.log(this.show60870);
        }
    }
});