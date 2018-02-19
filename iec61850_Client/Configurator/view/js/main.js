class server61850 {
    constructor(){
        this.host = {};
        this.port = {};
        this.apTitleR = '1.1.1.999.1';
        this.aeQualifierR = '12';
        this.pSelectorR = '1';
        this.sSelectorR = '1';
        this.tSelectorR = '0';
        this.apTitleL = '1.1.1.999';
        this.aeQualifierL = '12';
        this.pSelectorL = '1';
        this.sSelectorL = '1';
        this.tSelectorL = '0';
        this.enabled = false;
        this.password = '';
        this.items61850 = [];
        this.itemsASDU = [];
    }
}

class ASDU{
    constructor(){
        this.typeID = {};
        this.vsq = {};
        this.cot = {};
        this.isNegative = {};
        this.isTest = {};
        this.oa = {};
        this.ca = {};
        this.objects = [];
    }
}

class Object61850{
    constructor(path, typeFC, typeMMS) {
        this.path = path;
        this.typeFC = typeFC;
        this.typeMMS = typeMMS;
    }
}

data = {
    host:'localhost',
    port: 2404,
    maxQueue: 10,
    maxConnection: 1,
    statusTls: false,
    servers61850:[]
};

actual = {
    index: 0,
    add_item61850: []
};

class info_add_item61850{
    constructor(){
        this.path ='';
        this.typeFC = 'NONE';
        this.typeMMS = 'MMS_ARRAY';
        this.show = false;
        this.edit_item_iec61850 = [];
    }
}

show = {
    show61850: [],
    show60870: false
};

start = {
    run: []
};

options_iec61850 = {
    options_fc: [
        { text: 'NONE', value: 'NONE'},
        { text: 'ST', value: 'ST'},
        { text: 'MX', value: 'MX'},
        { text: 'SP', value: 'SP'},
        { text: 'SV', value: 'SV'},
        { text: 'CF', value: 'CF'},
        { text: 'DC', value: 'DC'},
        { text: 'SG', value: 'SG'},
        { text: 'SE', value: 'SE'},
        { text: 'SR', value: 'SR'},
        { text: 'OR', value: 'OR'},
        { text: 'BL', value: 'BL'},
        { text: 'EX', value: 'EX'},
        { text: 'CO', value: 'CO'},
        { text: 'US', value: 'US'},
        { text: 'MS', value: 'MS'},
        { text: 'RP', value: 'RP'},
        { text: 'BR', value: 'BR'},
        { text: 'LG', value: 'LG'},
        { text: 'ALL', value: 'ALL'},
    ],
    options_mms: [
        { text: 'MMS_ARRAY', value: 'MMS_ARRAY'},
        { text: 'MMS_STRUCTURE', value: 'MMS_STRUCTURE'},
        { text: 'MMS_BOOLEAN', value: 'MMS_BOOLEAN'},
        { text: 'MMS_BIT_STRING', value: 'MMS_BIT_STRING'},
        { text: 'MMS_INTEGER', value: 'MMS_INTEGER'},
        { text: 'MMS_UNSIGNED', value: 'MMS_UNSIGNED'},
        { text: 'MMS_FLOAT', value: 'MMS_FLOAT'},
        { text: 'MMS_OCTET_STRING', value: 'MMS_OCTET_STRING'},
        { text: 'MMS_VISIBLE_STRING', value: 'MMS_VISIBLE_STRING'},
        { text: 'MMS_GENERALIZED_TIME', value: 'MMS_GENERALIZED_TIME'},
        { text: 'MMS_BINARY_TIME', value: 'MMS_BINARY_TIME'},
        { text: 'MMS_BCD', value: 'MMS_BCD'},
        { text: 'MMS_OBJ_ID', value: 'MMS_OBJ_ID'},
        { text: 'MMS_STRING', value: 'MMS_STRING'},
        { text: 'MMS_UTC_TIME', value: 'MMS_UTC_TIME'},
        { text: 'MMS_DATA_ACCESS_ERROR', value: 'MMS_DATA_ACCESS_ERROR'}
    ]
};

map = {
    data: data,
    actual: actual,
    show: show,
    start: start,
    options_iec61850: options_iec61850
};

class openStatus {
    constructor(){
        this.show61850 = new openShow61850();
        this.show60870 = [];
    }
}

class openShow61850{
    constructor(){
        this.ld = [];
        this.ln = [];
        this.do = [];
    }
}

class treeView{
    constructor(){
        this.title = {};
        this.node = [];
        this.show = {};
        this.type = {};
    }
}

open_view = false;
TreeView = [];

Vue.component('app-iec61850',{
    template: '#iec61850',
    data: function () {
        return {
            map: map,
            open_view: open_view,
            TreeView: TreeView
        }
    },
    methods: {
        startServer: async function () {
            let index = map.actual.index;
            let output = JSON.stringify( map.data.servers61850[index]);
            if(!map.start.run[index])
            {
                //Установка соединения с сервером
                let status = ! map.start.run[index];
                let list = await serverStart.startServer61850(output, status, index);
                let temp = JSON.parse(list);
                if(!temp)
                {
                    //map.start.run[index] = !map.start.run[index];
                    //Сервер не запустился
                    return;
                }
                TreeView[index].node = [];
                map.actual.add_item61850[index].edit_item_iec61850 = [];
                map.data.servers61850[index].items61850 = [];
                for(let i=0; i < temp.length; i++)
                {
                    this.add_new_item(temp[i].path, temp[i].typeFC, temp[i].typeMMS, index);
                }
            }
            else
            {
                let status = ! map.start.run[index];
                let list = await serverStart.startServer61850(output, status, index);
                let temp = JSON.parse(list);
                if(!temp)
                {
                    //Соединение не получилось разорвать
                    return;
                }
            }
            map.start.run[index] = !map.start.run[index];
            map.data.servers61850.sort();
        },
        removeServer: async function () {
            let index = map.actual.index;
            let temp = await serverStart.removeServer61850(index);
            if(temp !== false)
            {
                map.start.run[index] = false;
                map.start.run.splice(index, 1);
                TreeView.splice(index, 1);
                map.show.show61850[index] = false;
                this.$emit('showstatuschange', map.show.show61850[index]);
                map.show.show61850.splice(index, 1);
                map.data.servers61850.splice(index, 1);
                map.actual.add_item61850.splice(index, 1);
                map.data.servers61850.sort();
                //Запуск или остановка сервера
            }
        },
        add_show_new_item: function () {
            //Отобразить форму для создания нового атрибута
            map.actual.add_item61850[map.actual.index].show = true;
        },
        add_new_item: function (path, typeFC, typeMMS, index) {
            map.actual.add_item61850[index].edit_item_iec61850.push(false);
            //Добовляем элемент в массив
            let obj = new Object61850(
                path,
                typeFC ,
                typeMMS);

            function do_TreeView(index, obj, strLD, strLN) {
                let strDO = obj.path.split('/')[1].split('.')[1];
                //Проверка на существование DO
                if(!TreeView[index].node.find(ld_node => ld_node.title === strLD)
                        .node.find(ln_node => ln_node.title === strLN).node.find(doo => doo.title === strDO)) {
                    let objDO = new treeView();
                    objDO.show = false;
                    objDO.title = strDO;
                    objDO.type = "do";
                    TreeView[index].node.find(ld_node => ld_node.title === strLD)
                        .node.find(ln_node => ln_node.title === strLN).node.push(objDO);
                }
                //Кладем указатель на орбъект
                map.data.servers61850[index].items61850.push(obj);
                let index_obj = map.data.servers61850[index].items61850.indexOf(obj);

                TreeView[index].node.find(ld_node => ld_node.title === strLD)
                    .node.find(ln_node => ln_node.title === strLN)
                    .node.find(do_node => do_node.title === strDO)
                    .node.push({attr: map.data.servers61850[index].items61850[index_obj], show: false, edit: false});
             }

            function ln_TreeView(index, obj, strLD) {
                let strLN = obj.path.split('/')[1].split('.')[0];
                //Проверка на существование LN
                if(!TreeView[index].node.find(ld_node => ld_node.title === strLD).node.find(ln => ln.title === strLN)){
                    let objLN = new treeView();
                    objLN.show = false;
                    objLN.title = strLN;
                    objLN.type = "ln";
                    TreeView[index].node.find(ld_node => ld_node.title === strLD).node.push(objLN);
                }
                do_TreeView(index, obj, strLD, strLN);
            }

            function ld_TreeView(index, obj) {
                if(TreeView[index].type === "root"){
                    //Достаем имя LD
                    let strLD = obj.path.split('/')[0];
                    //Проверка на существование LD
                    if(!TreeView[index].node.find(ld => ld.title === strLD)){
                        let objLD = new treeView();
                        objLD.show = false;
                        objLD.title = strLD;
                        objLD.type = "ld";
                        TreeView[index].node.push(objLD);
                    }
                    ln_TreeView(index, obj, strLD);
                }
            }

            //Проверка на существование объекта
            if(!map.data.servers61850[index].items61850.includes(obj))
            {
                //Добовляем элемент в дерево отображения
                ld_TreeView(index, obj);
            }
            //Скрыть редактор элемента
            map.actual.add_item61850[index].show = false;
        },
        close_mew_item: function () {
            map.actual.add_item61850[map.actual.index].show = false;
        },
        edit_item_iec61850: function (index) {
            map.actual.add_item61850[map.actual.index].edit_item_iec61850[index] = !  map.actual.add_item61850[map.actual.index].edit_item_iec61850[index];
            map.data.servers61850[map.actual.index].items61850.sort();
            console.log(map.actual.add_item61850[ map.actual.index].edit_item_iec61850[index]);
        },
        remove_item_iec61850: function(itemAttr, indexAttr, indexDO, indexLN, indexLD){
            let index = map.actual.index;
            //Удаляем объект из списка элементов сервера
            let obj = TreeView[index].node[indexLD].node[indexLN].node[indexDO].node[indexAttr].attr;
            let indexItem = map.data.servers61850[index].items61850.indexOf(obj);
            map.data.servers61850[index].items61850.splice(indexItem, 1);
            map.data.servers61850[index].items61850.sort();

            //Удаляем элемент из отображения
            TreeView[index].node[indexLD].node[indexLN].node[indexDO].node.splice(indexAttr, 1);
            if(TreeView[index].node[indexLD].node[indexLN].node[indexDO].node.length === 0){
                TreeView[index].node[indexLD].node[indexLN].node.splice(indexDO, 1);
                if(TreeView[index].node[indexLD].node[indexLN].node.length === 0){
                    TreeView[index].node[indexLD].node.splice(indexLN, 1);
                    if(TreeView[index].node[indexLD].node.length === 0){
                        TreeView[index].node.splice(indexLD, 1);
                    }
                }
            }

        }
    }
});

Vue.component('app-header_left', {
    template: '#headerLeft',
    data: function () {
        return {
            map: map,
            open_view: open_view,
            TreeView: TreeView
        }
    },
    methods: {
        add_server61850: async function () {
            map.data.servers61850.push(new server61850());
            map.data.servers61850[data.servers61850.length - 1].host = "127.0.0.1";
            map.data.servers61850[data.servers61850.length - 1].port = 102;
            map.start.run.push(false);
            map.actual.add_item61850.push(new info_add_item61850());
            map.show.show61850.push(false);
            //Добавляем корень дерева атрибутов сервера
            let obj = new treeView();
            obj.show = true;
            obj.title = "root";
            obj.type = "root";
            TreeView.push(obj);
            //Добавить новый сервер


            var output = JSON.stringify(data.servers61850[data.servers61850.length - 1]);
            var temp = await serverStart.addServer61850(output);
            console.log(show.show61850[show.show61850.length - 1]);
        },
        show_server61850: function (server, index) {
            map.show.show61850[index] = !map.show.show61850[index];
            for(let i = 0; i < map.show.show61850.length; i++){
                if(i !== index){map.show.show61850[i] = false;}
            }
            console.log("lol" + map.show.show61850[index]);
            this.$emit('showstatuschange', map.show.show61850[index]);
            map.actual.index = index;
            map.data.servers61850.sort();

            console.log(index);
            console.log(server);
        }
    }
});

Vue.component('app-iec60870', {
    template: '#iec60870',
    data: function () {
        return map
    },
    methods: {
        changeHost: function () {
            console.log(map.data.host)
        },
        changePort: function () {
            console.log(map.data.port)
        }
    }
});

Vue.component('app-header_right', {
    template: '#headerRight',
    data: function () {
        return map
    },
    methods: {
        show_on: function () {
            map.show.show60870 = !map.show.show60870;
            this.$emit('showstatuschange',  map.show.show60870);
            map.data.servers61850.sort();

            console.log(map.show.show60870);
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
            console.log("test " + value);
            this.show60870 = value;
            if(this.show60870){
                this.show61850 = false;
                for(let i = 0; i < show.show61850.length; i++){
                    show.show61850[i] = false;
                }
            }
            console.log(this.show60870);
        },
        change_show61850: function (value) {
            console.log("test " + value);
            this.show61850 = value;
            if(this.show61850){
                this.show60870 = false
            }
            console.log(this.show61850);
        }
    }
});