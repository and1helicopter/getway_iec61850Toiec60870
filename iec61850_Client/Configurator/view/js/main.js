var data = {
    host:'localhost',
    port: 2404,
    maxQueue: 10,
    maxConnection: 1,
    statusTls: false,
    servers61850:[]
};

class server61850 {
    constructor(host, port){
        this.host = host;
        this.port = port;
        this.items = [];
    }
}

Vue.component('app-iec61850',{
    template: '#iec61850',
    data: function () {
        return data
    },
    methods: {

    }
});

Vue.component('app-header_left', {
    template: '#headerLeft',
    props: ["show"],
    data: function () {
        return data
    },
    methods: {
        add_server61850: function () {
            this.servers61850.push(new server61850('127.0.0.1', 102));
            console.log(this.servers61850[length - 1]);
        },
        show_server61850: function (server) {
            this.show = !this.show;
            this.$emit('showstatuschange', this.show);
            console.log(server);
        }
    }
});

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
        },
        change_show61850: function (value) {
            this.show61850 = value;
            if(this.show61850){
                this.show60870 = false
            }
            console.log(this.show61850);
        }
    }
});