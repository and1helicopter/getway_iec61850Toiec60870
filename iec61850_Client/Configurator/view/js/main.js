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

//ASDU
class ASDU{
    constructor(){
        this.typeID = 0;
        this.sq = false;
        this.length = 0;
        this.cot = 0;
        this.isNegative = false;
        this.isTest = false;
        this.oa = 0;
        this.ca = 0;
        this.objects = [];
        this.objectsInf = {
            typeID: {},
            sq: {},
            use: false
        }
    }
}

class InformationObject{
    constructor(){
        this.addrObj = {};  //addr - obj
        this.attributeObj = [];
    }
}

class InformationElement{
    constructor(){
        this.typeElement = {};  //Много различных типов//3 типа: штамп времени (3-byte/7-byte), значение, качество (time, value, quanlity)
        this.attributeElement = {};
    }
}

class Elements{
    constructor(){
        this.typeID = {};
        this.Elements = [];
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

options_iec60870 = {
    options_typeID: [
        {textRU: 'Не определено', textEN: 'NONE', value: 0},
        {textRU: 'Одноэлементная информация', textEN: 'M_SP_NA_1', value: 1},
        {textRU: 'Одноэлементная информация с меткой времени', textEN: 'M_SP_TA_1', value: 2},
        {textRU: 'Двухэлементная информация', textEN: 'M_DP_NA_1', value: 3},
        {textRU: 'Двухэлементная информация с меткой времени', textEN: 'M_DP_TA_1', value: 4},
        {textRU: 'Информация о положение отпаек', textEN: 'M_ST_NA_1', value: 5},
        {textRU: 'Информация о положение отпаек с меткой времени', textEN: 'M_ST_TA_1', value: 6},
        {textRU: 'Строка из 32 бит', textEN: 'M_BO_NA_1', value: 7},
        {textRU: 'Строка из 32 бит с меткой времени', textEN: 'M_BO_TA_1', value: 8},
        {textRU: 'Значение измеряемой величины, нормализованное значение', textEN: 'M_ME_NA_1', value: 9},
        {textRU: 'Значение измеряемой величины, нормализованное значение с меткой времени', textEN: 'M_ME_TA_1', value: 10},
        {textRU: 'Значение измеряемой величины, масштабированное значение', textEN: 'M_ME_NB_1', value: 11},
        {textRU: 'Значение измеряемой величины, масштабированное значение с меткой времени', textEN: 'M_ME_TB_1', value: 12},
        {textRU: 'Значение измеряемой величины, короткий формат с плавающей запятой', textEN: 'M_ME_NC_1', value: 13},
        {textRU: 'Значение измеряемой величины, короткий формат с плавающей запятой с меткой времени', textEN: 'M_MC_TB_1', value: 14},
        {textRU: 'Интегральная сумма', textEN: 'M_IT_NA_1', value: 15},
        {textRU: 'Интегральная сумма запятой с меткой времени', textEN: 'M_IT_TA_1', value: 16},
        {textRU: 'Информация о работе релейной защиты с меткой времени', textEN: 'M_EP_TA_1', value: 17},
        {textRU: 'Упакованная информация о срабатывание пусковых органов защиты с меткой времени', textEN: 'M_IT_TB_1', value: 18},
        {textRU: 'Упакованная информация о срабатывание выходных цепей защиты с меткой времени', textEN: 'M_EP_TC_1', value: 19},
        {textRU: 'Упакованная одноэлементная информация суказателем изменения состояния', textEN: 'M_PS_NA_1', value: 20},
        {textRU: 'Значение измеряемой величины, нормализованное значение без описателя качества', textEN: 'M_ME_ND_1', value: 21},
        {textRU: 'Резерв для дальнейших совместимых определений', textEN: '', value: 22},
        {textRU: 'Резерв для дальнейших совместимых определений', textEN: '', value: 23},
        {textRU: 'Резерв для дальнейших совместимых определений', textEN: '', value: 24},
        {textRU: 'Резерв для дальнейших совместимых определений', textEN: '', value: 25},
        {textRU: 'Резерв для дальнейших совместимых определений', textEN: '', value: 26},
        {textRU: 'Резерв для дальнейших совместимых определений', textEN: '', value: 27},
        {textRU: 'Резерв для дальнейших совместимых определений', textEN: '', value: 28},
        {textRU: 'Резерв для дальнейших совместимых определений', textEN: '', value: 29},
        {textRU: 'Одноэлементная информация с меткой времени СР56Время2а', textEN: 'М_SP_ТB_1', value: 30},
        {textRU: 'Двухэлементная информация с меткой времени СР56Время2а', textEN: 'М_DP_TB_1', value: 31},
        {textRU: 'Информация о положении отпаек с меткой времени СР56Время2а', textEN: 'М_ST_TB_1', value: 32},
        {textRU: 'Строка из 32 бит с меткой времени СР56Время2а', textEN: 'М_ВО_TB_1', value: 33},
        {textRU: 'Значение измеряемой величины, нормализованное значение с меткой времени СР56Время2а', textEN: 'М_МЕ_TD_1', value: 34},
        {textRU: 'Значение измеряемой величины, масштабированное значение с меткой времени СР56Время2а', textEN: 'М_МЕ_TЕ_1', value: 35},
        {textRU: 'Значение измеряемой величины, короткий формат с плавающей запятой с меткой времени СР56Время2а', textEN: 'М_МЕ_TF_1', value: 36},
        {textRU: 'Интегральная сумма с меткой времени СР56Время2а', textEN: 'М_IT_TB_1', value: 37},
        {textRU: 'Информация о работе релейной защиты с меткой времени СР56Время2а', textEN: 'М_ЕР_TD_1', value: 38},
        {textRU: 'Упакованная информация о срабатывании пусковых органов защиты с меткой времени СР56Время2а', textEN: 'М_ЕР_TЕ_1', value: 39},
        {textRU: 'Упакованная информация о срабатывании выходных цепей защиты с меткой времени СР56Время2а', textEN: 'М_ЕР_TF_1', value: 40},
        {textRU: 'Резерв для дальнейших совместимых определений', textEN: '', value: 41},
        {textRU: 'Резерв для дальнейших совместимых определений', textEN: '', value: 42},
        {textRU: 'Резерв для дальнейших совместимых определений', textEN: '', value: 43},
        {textRU: 'Резерв для дальнейших совместимых определений', textEN: '', value: 44},
        {textRU: 'Однопозиционная команда', textEN: 'C_SC_NA_1', value: 45},
        {textRU: 'Двухпозиционная команда', textEN: 'C_DC_NA_1', value: 46},
        {textRU: 'Команда пошагового регулирования', textEN: 'C_RC_NA_1', value: 47},
        {textRU: 'Команда уставки, нормализованное значение', textEN: 'C_SE_NA_1', value: 48},
        {textRU: 'Команда уставки, масштабированное значение', textEN: 'C_SE_NB_1', value: 49},
        {textRU: 'Команда уставки, короткий формат с плавающей запятой', textEN: 'C_SE_NC_1', value: 50},
        {textRU: 'Строка из 32 бит', textEN: 'C_ВО_NA_1', value: 51},
        {textRU: 'Резерв для дальнейших совместимых определений', textEN: '', value: 52},
        {textRU: 'Резерв для дальнейших совместимых определений', textEN: '', value: 53},
        {textRU: 'Резерв для дальнейших совместимых определений', textEN: '', value: 54},
        {textRU: 'Резерв для дальнейших совместимых определений', textEN: '', value: 55},
        {textRU: 'Резерв для дальнейших совместимых определений', textEN: '', value: 56},
        {textRU: 'Резерв для дальнейших совместимых определений', textEN: '', value: 57},
        {textRU: 'Резерв для дальнейших совместимых определений', textEN: '', value: 58},
        {textRU: 'Резерв для дальнейших совместимых определений', textEN: '', value: 59},
        {textRU: 'Резерв для дальнейших совместимых определений', textEN: '', value: 60},
        {textRU: 'Резерв для дальнейших совместимых определений', textEN: '', value: 61},
        {textRU: 'Резерв для дальнейших совместимых определений', textEN: '', value: 62},
        {textRU: 'Резерв для дальнейших совместимых определений', textEN: '', value: 63},
        {textRU: 'Резерв для дальнейших совместимых определений', textEN: '', value: 64},
        {textRU: 'Резерв для дальнейших совместимых определений', textEN: '', value: 65},
        {textRU: 'Резерв для дальнейших совместимых определений', textEN: '', value: 66},
        {textRU: 'Резерв для дальнейших совместимых определений', textEN: '', value: 67},
        {textRU: 'Резерв для дальнейших совместимых определений', textEN: '', value: 68},
        {textRU: 'Резерв для дальнейших совместимых определений', textEN: '', value: 69},
        {textRU: 'Конец инициализации', textEN: 'М_ЕI_NA_1', value: 70},
        {textRU: 'Резерв для дальнейших совместимых определений', textEN: '', value: 71},
        {textRU: 'Резерв для дальнейших совместимых определений', textEN: '', value: 72},
        {textRU: 'Резерв для дальнейших совместимых определений', textEN: '', value: 73},
        {textRU: 'Резерв для дальнейших совместимых определений', textEN: '', value: 74},
        {textRU: 'Резерв для дальнейших совместимых определений', textEN: '', value: 75},
        {textRU: 'Резерв для дальнейших совместимых определений', textEN: '', value: 76},
        {textRU: 'Резерв для дальнейших совместимых определений', textEN: '', value: 77},
        {textRU: 'Резерв для дальнейших совместимых определений', textEN: '', value: 78},
        {textRU: 'Резерв для дальнейших совместимых определений', textEN: '', value: 79},
        {textRU: 'Резерв для дальнейших совместимых определений', textEN: '', value: 80},
        {textRU: 'Резерв для дальнейших совместимых определений', textEN: '', value: 81},
        {textRU: 'Резерв для дальнейших совместимых определений', textEN: '', value: 82},
        {textRU: 'Резерв для дальнейших совместимых определений', textEN: '', value: 83},
        {textRU: 'Резерв для дальнейших совместимых определений', textEN: '', value: 84},
        {textRU: 'Резерв для дальнейших совместимых определений', textEN: '', value: 85},
        {textRU: 'Резерв для дальнейших совместимых определений', textEN: '', value: 86},
        {textRU: 'Резерв для дальнейших совместимых определений', textEN: '', value: 87},
        {textRU: 'Резерв для дальнейших совместимых определений', textEN: '', value: 88},
        {textRU: 'Резерв для дальнейших совместимых определений', textEN: '', value: 89},
        {textRU: 'Резерв для дальнейших совместимых определений', textEN: '', value: 90},
        {textRU: 'Резерв для дальнейших совместимых определений', textEN: '', value: 91},
        {textRU: 'Резерв для дальнейших совместимых определений', textEN: '', value: 92},
        {textRU: 'Резерв для дальнейших совместимых определений', textEN: '', value: 93},
        {textRU: 'Резерв для дальнейших совместимых определений', textEN: '', value: 94},
        {textRU: 'Резерв для дальнейших совместимых определений', textEN: '', value: 95},
        {textRU: 'Резерв для дальнейших совместимых определений', textEN: '', value: 96},
        {textRU: 'Резерв для дальнейших совместимых определений', textEN: '', value: 97},
        {textRU: 'Резерв для дальнейших совместимых определений', textEN: '', value: 98},
        {textRU: 'Резерв для дальнейших совместимых определений', textEN: '', value: 99},
        {textRU: 'Команда опроса', textEN: 'C_IC_NA_1', value: 100},
        {textRU: 'Команда опроса счетчиков', textEN: 'C_CI_NA_1', value: 101},
        {textRU: 'Команда чтения', textEN: 'C_RD_NA_1', value: 102},
        {textRU: 'Команда синхронизации часов', textEN: 'C_CS_NA_1', value: 103},
        {textRU: 'Команда тестирования', textEN: 'C_ТS_NA_1', value: 104},
        {textRU: 'Команда сброса процесса в исходное состояние', textEN: 'C_RP_NA_1', value: 105},
        {textRU: 'Команда определения запаздывания', textEN: 'C_CD_NA_1', value: 106},
        {textRU: 'Резерв для дальнейших совместимых определений', textEN: '', value: 107},
        {textRU: 'Резерв для дальнейших совместимых определений', textEN: '', value: 108},
        {textRU: 'Резерв для дальнейших совместимых определений', textEN: '', value: 109},
        {textRU: 'Параметр измеряемой величины, нормализованное значение', textEN: 'Р_МЕ_NA_1', value: 110},
        {textRU: 'Параметр измеряемой величины, масштабированное значение', textEN: 'Р_МЕ_NВ_1', value: 111},
        {textRU: 'Параметр измеряемой величины, короткий формат с плавающей запятой', textEN: 'Р_МЕ_NС_1', value: 112},
        {textRU: 'Параметр активации', textEN: 'Р_АС_NА_1', value: 113},
        {textRU: 'Резерв для дальнейших совместимых определений', textEN: '', value: 114},
        {textRU: 'Резерв для дальнейших совместимых определений', textEN: '', value: 115},
        {textRU: 'Резерв для дальнейших совместимых определений', textEN: '', value: 116},
        {textRU: 'Резерв для дальнейших совместимых определений', textEN: '', value: 117},
        {textRU: 'Резерв для дальнейших совместимых определений', textEN: '', value: 118},
        {textRU: 'Резерв для дальнейших совместимых определений', textEN: '', value: 119},
        {textRU: 'Файл готов', textEN: 'F_FR_NA_1', value: 120},
        {textRU: 'Секция готова', textEN: 'F_SR_NA_1', value: 121},
        {textRU: 'Вызов директории, выбор файла, вызов файла, вызов секции', textEN: 'F_SC_NA_1', value: 122},
        {textRU: 'Последняя секция, последний сегмент', textEN: 'F_LS_NA_1', value: 123},
        {textRU: 'Подтверждение файла, подтверждение секции', textEN: 'F_AF_NA_1', value: 124},
        {textRU: 'Сегмент', textEN: 'F_SG_NA_1', value: 125},
        {textRU: 'Директория', textEN: 'F_DR_TA_1', value: 126},
        {textRU: 'Резерв для дальнейших совместимых определений', textEN: '', value: 127},
    ],
    options_cot: [
        {textRU: 'Не используется', textEN: 'NONE', value: 0},
        {textRU: 'Периодически, циклически', textEN: 'per/cyc', value: 1},
        {textRU: 'Фоновое сканирование', textEN: 'back', value: 2},
        {textRU: 'Спорадически', textEN: 'spont', value: 3},
        {textRU: 'Сообщение об инициализации', textEN: 'init', value: 4},
        {textRU: 'Запрос или запрашиваемые данные', textEN: 'req', value: 5},
        {textRU: 'Активация', textEN: 'act', value: 6},
        {textRU: 'Пдтверждение активации', textEN: 'actсon', value: 7},
        {textRU: 'Деактивация', textEN: 'deact', value: 8},
        {textRU: 'Подтверждение деактивации', textEN: 'deactcon', value: 9},
        {textRU: 'Завершение активации', textEN: 'actterm', value: 10},
        {textRU: 'Обратная информация, вызванная удаленной командой', textEN: 'retrem', value: 11},
        {textRU: 'Обратная информация, вызванная местной командой', textEN: 'retloc', value: 12},
        {textRU: 'Передача файлов', textEN: 'file', value: 13},
        {textRU: 'Резерв для дальнейших совместимых определений', textEN: '', value: 14},
        {textRU: 'Резерв для дальнейших совместимых определений', textEN: '', value: 15},
        {textRU: 'Резерв для дальнейших совместимых определений', textEN: '', value: 16},
        {textRU: 'Резерв для дальнейших совместимых определений', textEN: '', value: 17},
        {textRU: 'Резерв для дальнейших совместимых определений', textEN: '', value: 18},
        {textRU: 'Резерв для дальнейших совместимых определений', textEN: '', value: 19},
        {textRU: 'Ответ на опрос станции', textEN: 'inrogen', value: 20},
        {textRU: 'Ответ на опрос станции 1', textEN: 'inro1', value: 21},
        {textRU: 'Ответ на опрос станции 2', textEN: 'inro2', value: 22},
        {textRU: 'Ответ на опрос станции 3', textEN: 'inro3', value: 23},
        {textRU: 'Ответ на опрос станции 4', textEN: 'inro4', value: 24},
        {textRU: 'Ответ на опрос станции 5', textEN: 'inro5', value: 25},
        {textRU: 'Ответ на опрос станции 6', textEN: 'inro6', value: 26},
        {textRU: 'Ответ на опрос станции 7', textEN: 'inro7', value: 27},
        {textRU: 'Ответ на опрос станции 8', textEN: 'inro8', value: 28},
        {textRU: 'Ответ на опрос станции 9', textEN: 'inro9', value: 29},
        {textRU: 'Ответ на опрос станции 10', textEN: 'inro10', value: 30},
        {textRU: 'Ответ на опрос станции 11', textEN: 'inro11', value: 31},
        {textRU: 'Ответ на опрос станции 12', textEN: 'inro12', value: 32},
        {textRU: 'Ответ на опрос станции 13', textEN: 'inro13', value: 33},
        {textRU: 'Ответ на опрос станции 14', textEN: 'inro14', value: 34},
        {textRU: 'Ответ на опрос станции 15', textEN: 'inro15', value: 35},
        {textRU: 'Ответ на опрос станции 16', textEN: 'inro16', value: 36},
        {textRU: 'Ответ на общий запрос счетчиков', textEN: 'reqcogen', value: 37},
        {textRU: 'Ответ на запрос группы счетчиков 1', textEN: 'reqco1', value: 38},
        {textRU: 'Ответ на запрос группы счетчиков 2', textEN: 'reqco2', value: 39},
        {textRU: 'Ответ на запрос группы счетчиков 3', textEN: 'reqco3', value: 40},
        {textRU: 'Ответ на запрос группы счетчиков 4', textEN: 'reqco4', value: 41},
        {textRU: 'Резерв для дальнейших совместимых определений', textEN: '', value: 42},
        {textRU: 'Резерв для дальнейших совместимых определений', textEN: '', value: 43},
        {textRU: 'Неизвестный идентификатор типа', textEN: '', value: 44},
        {textRU: 'Неизвестная причина передачи', textEN: '', value: 45},
        {textRU: 'Неизвестный общий адрес ASDU', textEN: '', value: 46},
        {textRU: 'Неизвестный адрес объекта информаци', textEN: '', value: 47},
        {textRU: 'Для специальных применений (частный диапазон)', textEN: '', value: 48},
        {textRU: 'Для специальных применений (частный диапазон)', textEN: '', value: 49},
        {textRU: 'Для специальных применений (частный диапазон)', textEN: '', value: 50},
        {textRU: 'Для специальных применений (частный диапазон)', textEN: '', value: 51},
        {textRU: 'Для специальных применений (частный диапазон)', textEN: '', value: 52},
        {textRU: 'Для специальных применений (частный диапазон)', textEN: '', value: 53},
        {textRU: 'Для специальных применений (частный диапазон)', textEN: '', value: 54},
        {textRU: 'Для специальных применений (частный диапазон)', textEN: '', value: 55},
        {textRU: 'Для специальных применений (частный диапазон)', textEN: '', value: 56},
        {textRU: 'Для специальных применений (частный диапазон)', textEN: '', value: 57},
        {textRU: 'Для специальных применений (частный диапазон)', textEN: '', value: 58},
        {textRU: 'Для специальных применений (частный диапазон)', textEN: '', value: 59},
        {textRU: 'Для специальных применений (частный диапазон)', textEN: '', value: 60},
        {textRU: 'Для специальных применений (частный диапазон)', textEN: '', value: 61},
        {textRU: 'Для специальных применений (частный диапазон)', textEN: '', value: 62},
        {textRU: 'Для специальных применений (частный диапазон)', textEN: '', value: 63}
    ]
};

map = {
    data: data,
    actual: actual,
    show: show,
    start: start,
    options_iec61850: options_iec61850,
    options_iec60870: options_iec60870
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

class showASDU{
    constructor(){
        this.current = 0;
        this.total = 0;
        this.show = false;
        this.show_left = false;
        this.show_right = false;
    }
}

open_view = false;
TreeView = [];
ShowASDU = [];
TempASDU_COT = [];

Vue.component('app-iec61850',{
    template: '#iec61850',
    data: function () {
        return {
            map: map,
            open_view: open_view,
            TreeView: TreeView,
            ShowASDU: ShowASDU,
            TempASDU_COT: TempASDU_COT
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

        },
        //функции для ASDU
        add_asdu: function (index) {
            let asduObj = new ASDU();
            map.data.servers61850[index].itemsASDU.push(asduObj);
            ShowASDU[index].total += 1;
        },
        change_current: function (indexASDU, index) {
            ShowASDU[index].current = indexASDU;
            ShowASDU[index].show = true;
        },
        show_asdu: function () {
            return this.total > 0;
        },
        show_text: function (id, obj) {
            for(let i = 0; i < obj.length; i++){
                if(obj[i].value == id){
                   let strEN = obj[i].textEN;
                   let strRU = obj[i].textRU;
                   console.log({strEN, strRU});
                    return {strEN, strRU};
                }
            }
        },
        cot_filter: function (value) {
            this.TempASDU_COT = [];
            if (value === '0' || value === '22' || value === '23' || value === '24' || value === '25' || value === '26' || value === '27' || value === '28'
                || value === '29' || value === '41' || value === '42' || value === '43' || value === '44'
                || value === '52' || value === '53' || value === '54' || value === '55' || value === '56' || value === '57' || value === '58' || value === '59'
                || value === '60' || value === '61' || value === '62' || value === '63' || value === '64' || value === '65' || value === '66' || value === '67'
                || value === '68' || value === '69' || value === '71' || value === '72' || value === '73' || value === '74' || value === '75' || value === '76'
                || value === '77' || value === '78' || value === '79' || value === '80' || value === '81' || value === '82' || value === '83' || value === '84'
                || value === '85' || value === '86' || value === '87' || value === '88' || value === '89' || value === '90' || value === '91' || value === '92'
                || value === '93' || value === '94' || value === '95' || value === '96' || value === '97' || value === '98' || value === '99' || value === '107'
                || value === '108' || value === '109' || value === '114' || value === '115' || value === '116' || value === '117' || value === '118' || value === '119'){
                this.TempASDU_COT.push(map.options_iec60870.options_cot[0]);
                return false;
            }
            else if(value === '1' || value === '3' || value === '5' || value === '20') {
                for(let i = 0; i < map.options_iec60870.options_cot.length; i++){
                    let tempVal = map.options_iec60870.options_cot[i].value;
                    if(tempVal === 2 || tempVal === 3 || tempVal === 5 || tempVal === 11 || tempVal === 12 || tempVal === 20 || tempVal === 21 || tempVal === 22
                        || tempVal === 23 || tempVal === 24 || tempVal === 25 || tempVal === 26 || tempVal === 27 || tempVal === 28 || tempVal === 29
                        || tempVal === 30 || tempVal === 31 || tempVal === 32 || tempVal === 33 || tempVal === 34 || tempVal === 35 || tempVal === 36) {
                            this.TempASDU_COT.push(map.options_iec60870.options_cot[i]);
                    }
                }
            }
            else if(value === '2' || value === '4' || value === '6' || value === '30' || value === '31' || value === '32') {
                for(let i = 0; i < map.options_iec60870.options_cot.length; i++){
                    let tempVal = map.options_iec60870.options_cot[i].value;
                    if(tempVal === 3 || tempVal === 5 || tempVal === 11 || tempVal === 12) {
                        this.TempASDU_COT.push(map.options_iec60870.options_cot[i]);
                    }
                }
            }
            else if(value === '7') {
                for(let i = 0; i < map.options_iec60870.options_cot.length; i++){
                    let tempVal = map.options_iec60870.options_cot[i].value;
                    if(tempVal === 2 || tempVal === 3 || tempVal === 5 || tempVal === 20 || tempVal === 21 || tempVal === 22 || tempVal === 23 || tempVal === 24
                        || tempVal === 25 || tempVal === 26 || tempVal === 27 || tempVal === 28 || tempVal === 29 || tempVal === 30
                        || tempVal === 31 || tempVal === 32 || tempVal === 33 || tempVal === 34 || tempVal === 35 || tempVal === 36) {
                        this.TempASDU_COT.push(map.options_iec60870.options_cot[i]);
                    }
                }
            }
            else if(value === '8' || value === '10' || value === '12' || value === '14' || value === '33' || value === '34' || value === '35' || value === '36' || value === '126') {
                for(let i = 0; i < map.options_iec60870.options_cot.length; i++){
                    let tempVal = map.options_iec60870.options_cot[i].value;
                    if(tempVal === 3 || tempVal === 5) {
                        this.TempASDU_COT.push(map.options_iec60870.options_cot[i]);
                    }
                }
            }
            else if(value === '9' || value === '11' || value === '13' || value === '21') {
                for(let i = 0; i < map.options_iec60870.options_cot.length; i++){
                    let tempVal = map.options_iec60870.options_cot[i].value;
                    if(tempVal === 1 || tempVal === 2 || tempVal === 3 || tempVal === 5 || tempVal === 20 || tempVal === 21 || tempVal === 22
                        || tempVal === 23 || tempVal === 24 || tempVal === 25 || tempVal === 26 || tempVal === 27 || tempVal === 28 || tempVal === 29
                        || tempVal === 30 || tempVal === 31 || tempVal === 32 || tempVal === 33 || tempVal === 34 || tempVal === 35 || tempVal === 36) {
                        this.TempASDU_COT.push(map.options_iec60870.options_cot[i]);
                    }
                }
            }
            else if(value === '15' || value === '16' || value === '37') {
                for(let i = 0; i < map.options_iec60870.options_cot.length; i++){
                    let tempVal = map.options_iec60870.options_cot[i].value;
                    if(tempVal === 3 || tempVal === 37 || tempVal === 38 || tempVal === 39 || tempVal === 40 || tempVal === 41) {
                        this.TempASDU_COT.push(map.options_iec60870.options_cot[i]);
                    }
                }
            }
            else if(value === '17' || value === '18' || value === '19' || value === '38' || value === '39' || value === '40') {
                for(let i = 0; i < map.options_iec60870.options_cot.length; i++){
                    let tempVal = map.options_iec60870.options_cot[i].value;
                    if(tempVal === 3) {
                        this.TempASDU_COT.push(map.options_iec60870.options_cot[i]);
                    }
                }
            }
            else if(value === '45' || value === '46' || value === '47' || value === '48' || value === '49' || value === '50' || value === '51' || value === '100' || value === '113') {
                for(let i = 0; i < map.options_iec60870.options_cot.length; i++){
                    let tempVal = map.options_iec60870.options_cot[i].value;
                    if(tempVal === 6 || tempVal === 7 || tempVal === 8 || tempVal === 9 || tempVal === 10 || tempVal === 44 || tempVal === 45 || tempVal === 46 || tempVal === 47) {
                        this.TempASDU_COT.push(map.options_iec60870.options_cot[i]);
                    }
                }
            }
            else if(value === '70') {
                for(let i = 0; i < map.options_iec60870.options_cot.length; i++){
                    let tempVal = map.options_iec60870.options_cot[i].value;
                    if(tempVal === 4) {
                        this.TempASDU_COT.push(map.options_iec60870.options_cot[i]);
                    }
                }
            }
            else if(value === '101') {
                for(let i = 0; i < map.options_iec60870.options_cot.length; i++){
                    let tempVal = map.options_iec60870.options_cot[i].value;
                    if(tempVal === 6 || tempVal === 7 || tempVal === 10 || tempVal === 44 || tempVal === 45 || tempVal === 46 || tempVal === 47) {
                        this.TempASDU_COT.push(map.options_iec60870.options_cot[i]);
                    }
                }
            }
            else if(value === '102') {
                for(let i = 0; i < map.options_iec60870.options_cot.length; i++){
                    let tempVal = map.options_iec60870.options_cot[i].value;
                    if(tempVal === 5) {
                        this.TempASDU_COT.push(map.options_iec60870.options_cot[i]);
                    }
                }
            }
            else if(value === '103' || value === '106') {
                for(let i = 0; i < map.options_iec60870.options_cot.length; i++){
                    let tempVal = map.options_iec60870.options_cot[i].value;
                    if(tempVal === 3 || tempVal === 6 || tempVal === 7 || tempVal === 44 || tempVal === 45 || tempVal === 46 || tempVal === 47) {
                        this.TempASDU_COT.push(map.options_iec60870.options_cot[i]);
                    }
                }
            }
            else if(value === '104' || value === '105') {
                for(let i = 0; i < map.options_iec60870.options_cot.length; i++){
                    let tempVal = map.options_iec60870.options_cot[i].value;
                    if(tempVal === 6 || tempVal === 7 || tempVal === 44 || tempVal === 45 || tempVal === 46 || tempVal === 47) {
                        this.TempASDU_COT.push(map.options_iec60870.options_cot[i]);
                    }
                }
            }
            else if(value === '110' || value === '111' || value === '112') {
                for(let i = 0; i < map.options_iec60870.options_cot.length; i++){
                    let tempVal = map.options_iec60870.options_cot[i].value;
                    if(tempVal === 3 || tempVal === 5 || tempVal === 6 || tempVal === 7 || tempVal === 20 || tempVal === 21 || tempVal === 22
                        || tempVal === 23 || tempVal === 24 || tempVal === 25 || tempVal === 26 || tempVal === 27 || tempVal === 28 || tempVal === 29
                        || tempVal === 30 || tempVal === 31 || tempVal === 32 || tempVal === 33 || tempVal === 34 || tempVal === 35 || tempVal === 36
                        || tempVal === 44 || tempVal === 45 || tempVal === 46 || tempVal === 47) {
                        this.TempASDU_COT.push(map.options_iec60870.options_cot[i]);
                    }
                }
            }
            else if(value === '120' || value === '121' || value === '123' || value === '124' || value === '125') {
                for(let i = 0; i < map.options_iec60870.options_cot.length; i++){
                    let tempVal = map.options_iec60870.options_cot[i].value;
                    if(tempVal === 13 || tempVal === 44 || tempVal === 45 || tempVal === 46 || tempVal === 47) {
                        this.TempASDU_COT.push(map.options_iec60870.options_cot[i]);
                    }
                }
            }
            else if(value === '122') {
                for(let i = 0; i < map.options_iec60870.options_cot.length; i++){
                    let tempVal = map.options_iec60870.options_cot[i].value;
                    if(tempVal === 5 || tempVal === 13 || tempVal === 44 || tempVal === 45 || tempVal === 46 || tempVal === 47) {
                        this.TempASDU_COT.push(map.options_iec60870.options_cot[i]);
                    }
                }
            }
            return true;
        },
        add_obj_asdu: function (index) {
            let indexASDU = this.ShowASDU[index].current;
            let typeID = map.data.servers61850[index].itemsASDU[indexASDU].typeID;
            let sq = map.data.servers61850[index].itemsASDU[indexASDU].sq;
            let count = map.data.servers61850[index].itemsASDU[indexASDU].objects.length;
            let Elements = [];

            if(typeID.toString() === '0' || typeID === '22' || typeID === '23' || typeID === '24' || typeID === '25' || typeID === '26'
                || typeID === '27' || typeID === '28' || typeID === '29' || typeID === '41' || typeID === '42' || typeID === '43' || typeID === '44'
                || typeID === '52' || typeID === '53' || typeID === '54' || typeID === '55' || typeID === '56' || typeID === '57' || typeID === '58'
                || typeID === '59' || typeID === '60' || typeID === '61' || typeID === '62' || typeID === '63' || typeID === '64' || typeID === '65'
                || typeID === '66' || typeID === '67' || typeID === '68' || typeID === '69' || typeID === '71' || typeID === '72' || typeID === '73'
                || typeID === '74' || typeID === '75' || typeID === '76' || typeID === '77' || typeID === '78' || typeID === '79' || typeID === '80'
                || typeID === '81' || typeID === '82' || typeID === '83' || typeID === '84' || typeID === '85' || typeID === '86' || typeID === '87'
                || typeID === '88' || typeID === '89' || typeID === '90' || typeID === '91' || typeID === '92' || typeID === '93' || typeID === '93'
                || typeID === '94' || typeID === '95' || typeID === '96' || typeID === '97' || typeID === '98' || typeID === '99' || typeID === '107'
                || typeID === '108' || typeID === '109' || typeID === '114' || typeID === '115' || typeID === '116' || typeID === '117' || typeID === '118'
                || typeID === '119'){
                alert('Структура типа не определена');
                return;
            }
            else if(typeID === '1'){
                //Формирование SIQ
                let infElementTempSIQ = new InformationElement();
                infElementTempSIQ.typeElement = 'SIQ';  //SIQ
                infElementTempSIQ.attributeElement = '';
                Elements.push(infElementTempSIQ);
            }
            else if(typeID === '2'){
                //Формирование SIQ
                let infElementTempSIQ = new InformationElement();
                infElementTempSIQ.typeElement = 'SIQ';  //SIQ
                infElementTempSIQ.attributeElement = '';
                Elements.push(infElementTempSIQ);
                //Формирование CP24Time2a
                let infElementTempTime = new InformationElement();
                infElementTempTime.typeElement = 'CP24Time2a';  //CP24Time2a
                infElementTempTime.attributeElement = '';
                Elements.push(infElementTempTime);
                //Поддержка на элементы
                if(sq === true){
                    alert('Тип не поддерживает элементы');
                    return;
                }
            }
            else if(typeID === '3'){
                //Формирование DIQ
                let infElementTempDIQ = new InformationElement();
                infElementTempDIQ.typeElement = 'DIQ';  //DIQ
                infElementTempDIQ.attributeElement = '';
                Elements.push(infElementTempDIQ);
            }
            else if(typeID === '4'){
                //Формирование DIQ
                let infElementTempDIQ = new InformationElement();
                infElementTempDIQ.typeElement = 'DIQ';  //DIQ
                infElementTempDIQ.attributeElement = '';
                Elements.push(infElementTempDIQ);
                //Формирование CP24Time2a
                let infElementTempTime = new InformationElement();
                infElementTempTime.typeElement = 'CP24Time2a';  //CP24Time2a
                infElementTempTime.attributeElement = '';
                Elements.push(infElementTempTime);
                //Поддержка на элементы
                if(sq === true){
                    alert('Тип не поддерживает элементы');
                    return;
                }
            }
            else if(typeID === '5'){
                //Формирование VTI
                let infElementTempVTI = new InformationElement();
                infElementTempVTI.typeElement = 'VTI';  //VTI
                infElementTempVTI.attributeElement = '';
                Elements.push(infElementTempVTI);
                //Формирование QDS
                let infElementTempQDS = new InformationElement();
                infElementTempQDS.typeElement = 'QDS';  //QDS
                infElementTempQDS.attributeElement = '';
                Elements.push(infElementTempQDS);
            }
            else if(typeID === '6'){
                //Формирование VTI
                let infElementTempVTI = new InformationElement();
                infElementTempVTI.typeElement = 'VTI';  //VTI
                infElementTempVTI.attributeElement = '';
                Elements.push(infElementTempVTI);
                //Формирование QDS
                let infElementTempQDS = new InformationElement();
                infElementTempQDS.typeElement = 'QDS';  //QDS
                infElementTempQDS.attributeElement = '';
                Elements.push(infElementTempQDS);
                //Формирование CP24Time2a
                let infElementTempTime = new InformationElement();
                infElementTempTime.typeElement = 'CP24Time2a';  //CP24Time2a
                infElementTempTime.attributeElement = '';
                Elements.push(infElementTempTime);
                //Поддержка на элементы
                if(sq === true){
                    alert('Тип не поддерживает элементы');
                    return;
                }
            }
            else if (typeID === '7') {
                //Формирование BSI
                let infElementTempVTI = new InformationElement();
                infElementTempVTI.typeElement = 'BSI';  //BSI
                infElementTempVTI.attributeElement = '';
                Elements.push(infElementTempVTI);
                //Формирование QDS
                let infElementTempQDS = new InformationElement();
                infElementTempQDS.typeElement = 'QDS';  //QDS
                infElementTempQDS.attributeElement = '';
                Elements.push(infElementTempQDS);
            }
            else if (typeID === '8') {
                //Формирование BSI
                let infElementTempBSI = new InformationElement();
                infElementTempBSI.typeElement = 'BSI';  //BSI
                infElementTempBSI.attributeElement = '';
                Elements.push(infElementTempBSI);
                //Формирование QDS
                let infElementTempQDS = new InformationElement();
                infElementTempQDS.typeElement = 'QDS';  //QDS
                infElementTempQDS.attributeElement = '';
                Elements.push(infElementTempQDS);
                //Формирование CP24Time2a
                let infElementTempTime = new InformationElement();
                infElementTempTime.typeElement = 'CP24Time2a';  //CP24Time2a
                infElementTempTime.attributeElement = '';
                Elements.push(infElementTempTime);
                //Поддержка на элементы
                if(sq === true){
                    alert('Тип не поддерживает элементы');
                    return;
                }
            }
            else if (typeID === '9') {
                //Формирование NVA
                let infElementTempNVA = new InformationElement();
                infElementTempNVA.typeElement = 'NVA';  //NVA
                infElementTempNVA.attributeElement = '';
                Elements.push(infElementTempNVA);
                //Формирование QDS
                let infElementTempQDS = new InformationElement();
                infElementTempQDS.typeElement = 'QDS';  //QDS
                infElementTempQDS.attributeElement = '';
                Elements.push(infElementTempQDS);
            }
            else if (typeID === '10') {
                //Формирование NVA
                let infElementTempNVA = new InformationElement();
                infElementTempNVA.typeElement = 'NVA';  //NVA
                infElementTempNVA.attributeElement = '';
                Elements.push(infElementTempNVA);
                //Формирование QDS
                let infElementTempQDS = new InformationElement();
                infElementTempQDS.typeElement = 'QDS';  //QDS
                infElementTempQDS.attributeElement = '';
                Elements.push(infElementTempQDS);
                //Формирование CP24Time2a
                let infElementTempTime = new InformationElement();
                infElementTempTime.typeElement = 'CP24Time2a';  //CP24Time2a
                infElementTempTime.attributeElement = '';
                Elements.push(infElementTempTime);
                //Поддержка на элементы
                if(sq === true){
                    alert('Тип не поддерживает элементы');
                    return;
                }
            }
            else if (typeID === '11') {
                //Формирование SVA
                let infElementTempSVA = new InformationElement();
                infElementTempSVA.typeElement = 'SVA';  //SVA
                infElementTempSVA.attributeElement = '';
                Elements.push(infElementTempSVA);
                //Формирование QDS
                let infElementTempQDS = new InformationElement();
                infElementTempQDS.typeElement = 'QDS';  //QDS
                infElementTempQDS.attributeElement = '';
                Elements.push(infElementTempQDS);
            }
            else if (typeID === '12') {
                //Формирование SVA
                let infElementTempSVA = new InformationElement();
                infElementTempSVA.typeElement = 'SVA';  //SVA
                infElementTempSVA.attributeElement = '';
                Elements.push(infElementTempSVA);
                //Формирование QDS
                let infElementTempQDS = new InformationElement();
                infElementTempQDS.typeElement = 'QDS';  //QDS
                infElementTempQDS.attributeElement = '';
                Elements.push(infElementTempQDS);
                //Формирование CP24Time2a
                let infElementTempTime = new InformationElement();
                infElementTempTime.typeElement = 'CP24Time2a';  //CP24Time2a
                infElementTempTime.attributeElement = '';
                Elements.push(infElementTempTime);
                //Поддержка на элементы
                if(sq === true){
                    alert('Тип не поддерживает элементы');
                    return;
                }
            }
            else if (typeID === '13') {
                //Формирование IEEE STD 754
                let infElementTempIEEE = new InformationElement();
                infElementTempIEEE.typeElement = 'IEEE STD 754';  //IEEE STD 754
                infElementTempIEEE.attributeElement = '';
                Elements.push(infElementTempIEEE);
                //Формирование QDS
                let infElementTempQDS = new InformationElement();
                infElementTempQDS.typeElement = 'QDS';  //QDS
                infElementTempQDS.attributeElement = '';
                Elements.push(infElementTempQDS);
            }
            else if (typeID === '14') {
                //Формирование IEEE STD 754
                let infElementTempIEEE = new InformationElement();
                infElementTempIEEE.typeElement = 'IEEE STD 754';  //IEEE STD 754
                infElementTempIEEE.attributeElement = '';
                Elements.push(infElementTempIEEE);
                //Формирование QDS
                let infElementTempQDS = new InformationElement();
                infElementTempQDS.typeElement = 'QDS';  //QDS
                infElementTempQDS.attributeElement = '';
                Elements.push(infElementTempQDS);
                //Формирование CP24Time2a
                let infElementTempTime = new InformationElement();
                infElementTempTime.typeElement = 'CP24Time2a';  //CP24Time2a
                infElementTempTime.attributeElement = '';
                Elements.push(infElementTempTime);
                //Поддержка на элементы
                if(sq === true){
                    alert('Тип не поддерживает элементы');
                    return;
                }
            }
            else if (typeID === '15') {
                //Формирование BCR
                let infElementTempBCR = new InformationElement();
                infElementTempBCR.typeElement = 'BCR';  //BCR
                infElementTempBCR.attributeElement = '';
                Elements.push(infElementTempBCR);
            }
            else if (typeID === '16') {
                //Формирование BCR
                let infElementTempBCR = new InformationElement();
                infElementTempBCR.typeElement = 'BCR';  //BCR
                infElementTempBCR.attributeElement = '';
                Elements.push(infElementTempBCR);
                //Формирование CP24Time2a
                let infElementTempTime = new InformationElement();
                infElementTempTime.typeElement = 'CP24Time2a';  //CP24Time2a
                infElementTempTime.attributeElement = '';
                Elements.push(infElementTempTime);
                //Поддержка на элементы
                if(sq === true){
                    alert('Тип не поддерживает элементы');
                    return;
                }
            }
            else if (typeID === '17') {
                //Формирование SEP
                let infElementTempSEP = new InformationElement();
                infElementTempSEP.typeElement = 'SEP';  //SEP
                infElementTempSEP.attributeElement = '';
                Elements.push(infElementTempSEP);
                //Формирование CP16Time2a
                let infElementTempTimeCP16 = new InformationElement();
                infElementTempTimeCP16.typeElement = 'CP16Time2a';  //CP16Time2a
                infElementTempTimeCP16.attributeElement = '';
                Elements.push(infElementTempTimeCP16);
                //Формирование CP24Time2a
                let infElementTempTimeCP24 = new InformationElement();
                infElementTempTimeCP24.typeElement = 'CP24Time2a';  //CP24Time2a
                infElementTempTimeCP24.attributeElement = '';
                Elements.push(infElementTempTimeCP24);
            }
            else if (typeID === '18') {
                //Формирование SPE
                let infElementTempSPE = new InformationElement();
                infElementTempSPE.typeElement = 'SPE';  //SPE
                infElementTempSPE.attributeElement = '';
                Elements.push(infElementTempSPE);
                //Формирование QDP
                let infElementTempQDP = new InformationElement();
                infElementTempQDP.typeElement = 'QDP';  //CP24Time2a
                infElementTempQDP.attributeElement = '';
                Elements.push(infElementTempQDP);
                //Формирование CP16Time2a
                let infElementTempTimeCP16 = new InformationElement();
                infElementTempTimeCP16.typeElement = 'CP16Time2a';  //CP16Time2a
                infElementTempTimeCP16.attributeElement = '';
                Elements.push(infElementTempTimeCP16);
                //Формирование CP24Time2a
                let infElementTempTimeCP24 = new InformationElement();
                infElementTempTimeCP24.typeElement = 'CP24Time2a';  //CP24Time2a
                infElementTempTimeCP24.attributeElement = '';
                Elements.push(infElementTempTimeCP24);
                //Поддержка на элементы
                if(sq === true){
                    alert('Тип не поддерживает элементы');
                    return;
                }
                //Одиночный объект
                if(map.data.servers61850[index].itemsASDU[indexASDU].length > 0){
                    alert('Тип содержит только один объект');
                    return;
                }
            }
            else if(typeID === '19'){
                //Формирование OCI
                let infElementTempOCI = new InformationElement();
                infElementTempOCI.typeElement = 'OCI';  //OCI
                infElementTempOCI.attributeElement = '';
                Elements.push(infElementTempOCI);
                //Формирование QDP
                let infElementTempQDP = new InformationElement();
                infElementTempQDP.typeElement = 'QDP';  //CP24Time2a
                infElementTempQDP.attributeElement = '';
                Elements.push(infElementTempQDP);
                //Формирование CP16Time2a
                let infElementTempTimeCP16 = new InformationElement();
                infElementTempTimeCP16.typeElement = 'CP16Time2a';  //CP16Time2a
                infElementTempTimeCP16.attributeElement = '';
                Elements.push(infElementTempTimeCP16);
                //Формирование CP24Time2a
                let infElementTempTimeCP24 = new InformationElement();
                infElementTempTimeCP24.typeElement = 'CP24Time2a';  //CP24Time2a
                infElementTempTimeCP24.attributeElement = '';
                Elements.push(infElementTempTimeCP24);
                //Поддержка на элементы
                if(sq === true){
                    alert('Тип не поддерживает элементы');
                    return;
                }
                //Одиночный объект
                if(map.data.servers61850[index].itemsASDU[indexASDU].length > 0){
                    alert('Тип содержит только один объект');
                    return;
                }
            }
            else if (typeID === '20'){
                //Формирование SCD
                let infElementTempSCD = new InformationElement();
                infElementTempSCD.typeElement = 'SCD';  //SCD
                infElementTempSCD.attributeElement = '';
                Elements.push(infElementTempSCD);
                //Формирование QDS
                let infElementTempQDS = new InformationElement();
                infElementTempQDS.typeElement = 'QDS';  //QDS
                infElementTempQDS.attributeElement = '';
                Elements.push(infElementTempQDS);
            }
            else if (typeID === '21'){
                //Формирование NVA
                let infElementTempNVA = new InformationElement();
                infElementTempNVA.typeElement = 'NVA';  //NVA
                infElementTempNVA.attributeElement = '';
                Elements.push(infElementTempNVA);
            }
            else if (typeID === '30'){
                //Формирование SIQ
                let infElementTempSIQ = new InformationElement();
                infElementTempSIQ.typeElement = 'SIQ';  //SIQ
                infElementTempSIQ.attributeElement = '';
                Elements.push(infElementTempSIQ);
                //Формирование CP56Time2a
                let infElementTempTime = new InformationElement();
                infElementTempTime.typeElement = 'CP56Time2a';  //CP56Time2a
                infElementTempTime.attributeElement = '';
                Elements.push(infElementTempTime);
                //Поддержка на элементы
                if(sq === true){
                    alert('Тип не поддерживает элементы');
                    return;
                }
            }
            else if (typeID === '31'){
                //Формирование DIQ
                let infElementTempDIQ = new InformationElement();
                infElementTempDIQ.typeElement = 'DIQ';  //DIQ
                infElementTempDIQ.attributeElement = '';
                Elements.push(infElementTempDIQ);
                //Формирование CP56Time2a
                let infElementTempTime = new InformationElement();
                infElementTempTime.typeElement = 'CP56Time2a';  //CP56Time2a
                infElementTempTime.attributeElement = '';
                Elements.push(infElementTempTime);
                //Поддержка на элементы
                if(sq === true){
                    alert('Тип не поддерживает элементы');
                    return;
                }
            }
            else if (typeID === '32'){
                //Формирование VTI
                let infElementTempVTI = new InformationElement();
                infElementTempVTI.typeElement = 'VTI';  //VTI
                infElementTempVTI.attributeElement = '';
                Elements.push(infElementTempVTI);
                //Формирование QDS
                let infElementTempQDS = new InformationElement();
                infElementTempQDS.typeElement = 'QDS';  //QDS
                infElementTempQDS.attributeElement = '';
                Elements.push(infElementTempQDS);
                //Формирование CP56Time2a
                let infElementTempTimeCP56 = new InformationElement();
                infElementTempTimeCP56.typeElement = 'CP56Time2a';  //CP56Time2a
                infElementTempTimeCP56.attributeElement = '';
                Elements.push(infElementTempTimeCP56);
                //Поддержка на элементы
                if(sq === true){
                    alert('Тип не поддерживает элементы');
                    return;
                }
            }
            else if (typeID === '33') {
                //Формирование BSI
                let infElementTempBSI = new InformationElement();
                infElementTempBSI.typeElement = 'BSI';  //BSI
                infElementTempBSI.attributeElement = '';
                Elements.push(infElementTempBSI);
                //Формирование QDS
                let infElementTempQDS = new InformationElement();
                infElementTempQDS.typeElement = 'QDS';  //QDS
                infElementTempQDS.attributeElement = '';
                Elements.push(infElementTempQDS);
                //Формирование CP56Time2a
                let infElementTempTimeCP56 = new InformationElement();
                infElementTempTimeCP56.typeElement = 'CP56Time2a';  //CP56Time2a
                infElementTempTimeCP56.attributeElement = '';
                Elements.push(infElementTempTimeCP56);
                //Поддержка на элементы
                if(sq === true){
                    alert('Тип не поддерживает элементы');
                    return;
                }
            }
            else if (typeID === '34') {
                //Формирование NVA
                let infElementTempNVA = new InformationElement();
                infElementTempNVA.typeElement = 'NVA';  //NVA
                infElementTempNVA.attributeElement = '';
                Elements.push(infElementTempNVA);
                //Формирование QDS
                let infElementTempQDS = new InformationElement();
                infElementTempQDS.typeElement = 'QDS';  //QDS
                infElementTempQDS.attributeElement = '';
                Elements.push(infElementTempQDS);
                //Формирование CP56Time2a
                let infElementTempTimeCP56 = new InformationElement();
                infElementTempTimeCP56.typeElement = 'CP56Time2a';  //CP56Time2a
                infElementTempTimeCP56.attributeElement = '';
                Elements.push(infElementTempTimeCP56);
                //Поддержка на элементы
                if(sq === true){
                    alert('Тип не поддерживает элементы');
                    return;
                }
            }
            else if (typeID === '35') {
                //Формирование SVA
                let infElementTempSVA = new InformationElement();
                infElementTempSVA.typeElement = 'SVA';  //SVA
                infElementTempSVA.attributeElement = '';
                Elements.push(infElementTempSVA);
                //Формирование QDS
                let infElementTempQDS = new InformationElement();
                infElementTempQDS.typeElement = 'QDS';  //QDS
                infElementTempQDS.attributeElement = '';
                Elements.push(infElementTempQDS);
                //Формирование CP56Time2a
                let infElementTempTimeCP56 = new InformationElement();
                infElementTempTimeCP56.typeElement = 'CP56Time2a';  //CP56Time2a
                infElementTempTimeCP56.attributeElement = '';
                Elements.push(infElementTempTimeCP56);
                //Поддержка на элементы
                if(sq === true){
                    alert('Тип не поддерживает элементы');
                    return;
                }
            }
            else if (typeID === '36') {
                //Формирование IEEE STD 754
                let infElementTempIEEE = new InformationElement();
                infElementTempIEEE.typeElement = 'IEEE STD 754';  //IEEE STD 754
                infElementTempIEEE.attributeElement = '';
                Elements.push(infElementTempIEEE);
                //Формирование QDS
                let infElementTempQDS = new InformationElement();
                infElementTempQDS.typeElement = 'QDS';  //QDS
                infElementTempQDS.attributeElement = '';
                Elements.push(infElementTempQDS);
                //Формирование CP56Time2a
                let infElementTempTimeCP56 = new InformationElement();
                infElementTempTimeCP56.typeElement = 'CP56Time2a';  //CP56Time2a
                infElementTempTimeCP56.attributeElement = '';
                Elements.push(infElementTempTimeCP56);
                //Поддержка на элементы
                if(sq === true){
                    alert('Тип не поддерживает элементы');
                    return;
                }
            }
            else if (typeID === '37') {
                //Формирование BCR
                let infElementTempBCR = new InformationElement();
                infElementTempBCR.typeElement = 'BCR';  //BCR
                infElementTempBCR.attributeElement = '';
                Elements.push(infElementTempBCR);
                //Формирование CP56Time2a
                let infElementTempTimeCP56 = new InformationElement();
                infElementTempTimeCP56.typeElement = 'CP56Time2a';  //CP56Time2a
                infElementTempTimeCP56.attributeElement = '';
                Elements.push(infElementTempTimeCP56);
                //Поддержка на элементы
                if(sq === true){
                    alert('Тип не поддерживает элементы');
                    return;
                }
            }
            else if (typeID === '38') {
                //Формирование SEP
                let infElementTempSEP = new InformationElement();
                infElementTempSEP.typeElement = 'SEP';  //SEP
                infElementTempSEP.attributeElement = '';
                Elements.push(infElementTempSEP);
                //Формирование CP16Time2a
                let infElementTempTimeCP16 = new InformationElement();
                infElementTempTimeCP16.typeElement = 'CP16Time2a';  //CP16Time2a
                infElementTempTimeCP16.attributeElement = '';
                Elements.push(infElementTempTimeCP16);
                //Формирование CP56Time2a
                let infElementTempTimeCP56 = new InformationElement();
                infElementTempTimeCP56.typeElement = 'CP56Time2a';  //CP56Time2a
                infElementTempTimeCP56.attributeElement = '';
                Elements.push(infElementTempTimeCP56);
                //Поддержка на элементы
                if(sq === true){
                    alert('Тип не поддерживает элементы');
                    return;
                }
            }
            else if (typeID === '39') {
                //Формирование SPE
                let infElementTempSPE = new InformationElement();
                infElementTempSPE.typeElement = 'SPE';  //SPE
                infElementTempSPE.attributeElement = '';
                Elements.push(infElementTempSPE);
                //Формирование QDP
                let infElementTempQDP = new InformationElement();
                infElementTempQDP.typeElement = 'QDP';  //QDP
                infElementTempQDP.attributeElement = '';
                Elements.push(infElementTempQDP);
                //Формирование CP16Time2a
                let infElementTempTimeCP16 = new InformationElement();
                infElementTempTimeCP16.typeElement = 'CP16Time2a';  //CP16Time2a
                infElementTempTimeCP16.attributeElement = '';
                Elements.push(infElementTempTimeCP16);
                //Формирование CP56Time2a
                let infElementTempTimeCP56 = new InformationElement();
                infElementTempTimeCP56.typeElement = 'CP56Time2a';  //CP56Time2a
                infElementTempTimeCP56.attributeElement = '';
                Elements.push(infElementTempTimeCP56);
                //Поддержка на элементы
                if(sq === true){
                    alert('Тип не поддерживает элементы');
                    return;
                }
                //Одиночный объект
                if(map.data.servers61850[index].itemsASDU[indexASDU].length > 0){
                    alert('Тип содержит только один объект');
                    return;
                }
            }
            else if (typeID === '40') {
                //Формирование OCI
                let infElementTempOCI = new InformationElement();
                infElementTempOCI.typeElement = 'OCI';  //OCI
                infElementTempOCI.attributeElement = '';
                Elements.push(infElementTempOCI);
                //Формирование QDP
                let infElementTempQDP = new InformationElement();
                infElementTempQDP.typeElement = 'QDP';  //QDP
                infElementTempQDP.attributeElement = '';
                Elements.push(infElementTempQDP);
                //Формирование CP16Time2a
                let infElementTempTimeCP16 = new InformationElement();
                infElementTempTimeCP16.typeElement = 'CP16Time2a';  //CP16Time2a
                infElementTempTimeCP16.attributeElement = '';
                Elements.push(infElementTempTimeCP16);
                //Формирование CP56Time2a
                let infElementTempTimeCP56 = new InformationElement();
                infElementTempTimeCP56.typeElement = 'CP56Time2a';  //CP56Time2a
                infElementTempTimeCP56.attributeElement = '';
                Elements.push(infElementTempTimeCP56);
                //Поддержка на элементы
                if(sq === true){
                    alert('Тип не поддерживает элементы');
                    return;
                }
                //Одиночный объект
                if(map.data.servers61850[index].itemsASDU[indexASDU].length > 0){
                    alert('Тип содержит только один объект');
                    return;
                }
            }
            else if (typeID === '45') {
                //Формирование SCO
                let infElementTempSCO = new InformationElement();
                infElementTempSCO.typeElement = 'SCO';  //SCO
                infElementTempSCO.attributeElement = '';
                Elements.push(infElementTempSCO);
                //Поддержка на элементы
                if(sq === true){
                    alert('Тип не поддерживает элементы');
                    return;
                }
                //Одиночный объект
                if(map.data.servers61850[index].itemsASDU[indexASDU].length > 0){
                    alert('Тип содержит только один объект');
                    return;
                }
            }
            else if (typeID === '46') {
                //Формирование DCO
                let infElementTempDCO = new InformationElement();
                infElementTempDCO.typeElement = 'DCO';  //DCO
                infElementTempDCO.attributeElement = '';
                Elements.push(infElementTempDCO);
                //Поддержка на элементы
                if(sq === true){
                    alert('Тип не поддерживает элементы');
                    return;
                }
                //Одиночный объект
                if(map.data.servers61850[index].itemsASDU[indexASDU].length > 0){
                    alert('Тип содержит только один объект');
                    return;
                }
            }
            else if (typeID === '47') {
                //Формирование RCO
                let infElementTempRCO = new InformationElement();
                infElementTempRCO.typeElement = 'RCO';  //RCO
                infElementTempRCO.attributeElement = '';
                Elements.push(infElementTempRCO);
                //Поддержка на элементы
                if(sq === true){
                    alert('Тип не поддерживает элементы');
                    return;
                }
                //Одиночный объект
                if(map.data.servers61850[index].itemsASDU[indexASDU].length > 0){
                    alert('Тип содержит только один объект');
                    return;
                }
            }
            else if (typeID === '48') {
                //Формирование NVA
                let infElementTempNVA = new InformationElement();
                infElementTempNVA.typeElement = 'NVA';  //NVA
                infElementTempNVA.attributeElement = '';
                Elements.push(infElementTempNVA);
                //Формирование QOS
                let infElementTempQOS = new InformationElement();
                infElementTempQOS.typeElement = 'QOS';  //QOS
                infElementTempQOS.attributeElement = '';
                Elements.push(infElementTempQOS);
                //Поддержка на элементы
                if(sq === true){
                    alert('Тип не поддерживает элементы');
                    return;
                }
                //Одиночный объект
                if(map.data.servers61850[index].itemsASDU[indexASDU].length > 0){
                    alert('Тип содержит только один объект');
                    return;
                }
            }
            else if (typeID === '49') {
                //Формирование SVA
                let infElementTempSVA = new InformationElement();
                infElementTempSVA.typeElement = 'SVA';  //SVA
                infElementTempSVA.attributeElement = '';
                Elements.push(infElementTempSVA);
                //Формирование QOS
                let infElementTempQOS = new InformationElement();
                infElementTempQOS.typeElement = 'QOS';  //QOS
                infElementTempQOS.attributeElement = '';
                Elements.push(infElementTempQOS);
                //Поддержка на элементы
                if(sq === true){
                    alert('Тип не поддерживает элементы');
                    return;
                }
                //Одиночный объект
                if(map.data.servers61850[index].itemsASDU[indexASDU].length > 0){
                    alert('Тип содержит только один объект');
                    return;
                }
            }
            else if (typeID === '50') {
                //Формирование IEEE STD 754
                let infElementTempIEEE = new InformationElement();
                infElementTempIEEE.typeElement = 'IEEE STD 754';  //IEEE STD 754
                infElementTempIEEE.attributeElement = '';
                Elements.push(infElementTempIEEE);
                //Формирование QOS
                let infElementTempQOS = new InformationElement();
                infElementTempQOS.typeElement = 'QOS';  //QOS
                infElementTempQOS.attributeElement = '';
                Elements.push(infElementTempQOS);
                //Поддержка на элементы
                if(sq === true){
                    alert('Тип не поддерживает элементы');
                    return;
                }
                //Одиночный объект
                if(map.data.servers61850[index].itemsASDU[indexASDU].length > 0){
                    alert('Тип содержит только один объект');
                    return;
                }
            }
            else if (typeID === '51') {
                //Формирование BSI
                let infElementTempBSI = new InformationElement();
                infElementTempBSI.typeElement = 'BSI';  //BSI
                infElementTempBSI.attributeElement = '';
                Elements.push(infElementTempBSI);
                //Поддержка на элементы
                if(sq === true){
                    alert('Тип не поддерживает элементы');
                    return;
                }
                //Одиночный объект
                if(map.data.servers61850[index].itemsASDU[indexASDU].length > 0){
                    alert('Тип содержит только один объект');
                    return;
                }
            }
            else if (typeID === '70') {
                //Формирование COI
                let infElementTempCOI = new InformationElement();
                infElementTempCOI.typeElement = 'COI';  //COI
                infElementTempCOI.attributeElement = '';
                Elements.push(infElementTempCOI);
                //Поддержка на элементы
                if(sq === true){
                    alert('Тип не поддерживает элементы');
                    return;
                }
                //Одиночный объект
                if(map.data.servers61850[index].itemsASDU[indexASDU].length > 0){
                    alert('Тип содержит только один объект');
                    return;
                }
            }
            else if (typeID === '100') {
                //Формирование QOI
                let infElementTempQOI = new InformationElement();
                infElementTempQOI.typeElement = 'QOI';  //QOI
                infElementTempQOI.attributeElement = '';
                Elements.push(infElementTempQOI);
                //Поддержка на элементы
                if(sq === true){
                    alert('Тип не поддерживает элементы');
                    return;
                }
                //Одиночный объект
                if(map.data.servers61850[index].itemsASDU[indexASDU].length > 0){
                    alert('Тип содержит только один объект');
                    return;
                }
            }
            else if (typeID === '101') {
                //Формирование QCC
                let infElementTempQCC = new InformationElement();
                infElementTempQCC.typeElement = 'QCC';  //QCC
                infElementTempQCC.attributeElement = '';
                Elements.push(infElementTempQCC);
                //Поддержка на элементы
                if(sq === true){
                    alert('Тип не поддерживает элементы');
                    return;
                }
                //Одиночный объект
                if(map.data.servers61850[index].itemsASDU[indexASDU].length > 0){
                    alert('Тип содержит только один объект');
                    return;
                }
            }
            else if (typeID === '103') {
                //Формирование CP56Time2a
                let infElementTempTimeCP56 = new InformationElement();
                infElementTempTimeCP56.typeElement = 'CP56Time2a';  //CP56Time2a
                infElementTempTimeCP56.attributeElement = '';
                Elements.push(infElementTempTimeCP56);
                //Поддержка на элементы
                if(sq === true){
                    alert('Тип не поддерживает элементы');
                    return;
                }
                //Одиночный объект
                if(map.data.servers61850[index].itemsASDU[indexASDU].length > 0){
                    alert('Тип содержит только один объект');
                    return;
                }
            }
            else if (typeID === '104') {
                //Формирование FBP
                let infElementTempFBP = new InformationElement();
                infElementTempFBP.typeElement = 'FBP';  //FBP
                infElementTempFBP.attributeElement = '';
                Elements.push(infElementTempFBP);
                //Поддержка на элементы
                if(sq === true){
                    alert('Тип не поддерживает элементы');
                    return;
                }
                //Одиночный объект
                if(map.data.servers61850[index].itemsASDU[indexASDU].length > 0){
                    alert('Тип содержит только один объект');
                    return;
                }
            }
            else if (typeID === '105') {
                //Формирование QRP
                let infElementTempQRP = new InformationElement();
                infElementTempQRP.typeElement = 'QRP';  //QRP
                infElementTempQRP.attributeElement = '';
                Elements.push(infElementTempQRP);
                //Поддержка на элементы
                if(sq === true){
                    alert('Тип не поддерживает элементы');
                    return;
                }
                //Одиночный объект
                if(map.data.servers61850[index].itemsASDU[indexASDU].length > 0){
                    alert('Тип содержит только один объект');
                    return;
                }
            }
            else if (typeID === '106') {
                //Формирование CP16Time2a
                let infElementTempTimeCP16 = new InformationElement();
                infElementTempTimeCP16.typeElement = 'CP16Time2a';  //CP16Time2a
                infElementTempTimeCP16.attributeElement = '';
                Elements.push(infElementTempTimeCP16);
                //Поддержка на элементы
                if(sq === true){
                    alert('Тип не поддерживает элементы');
                    return;
                }
                //Одиночный объект
                if(map.data.servers61850[index].itemsASDU[indexASDU].length > 0){
                    alert('Тип содержит только один объект');
                    return;
                }
            }
            else if (typeID === '110') {
                //Формирование NVA
                let infElementTempNVA = new InformationElement();
                infElementTempNVA.typeElement = 'NVA';  //NVA
                infElementTempNVA.attributeElement = '';
                Elements.push(infElementTempNVA);
                //Формирование QPM
                let infElementTempQPM = new InformationElement();
                infElementTempQPM.typeElement = 'QPM';  //QPM
                infElementTempQPM.attributeElement = '';
                Elements.push(infElementTempQPM);
                //Поддержка на элементы
                if(sq === true){
                    alert('Тип не поддерживает элементы');
                    return;
                }
                //Одиночный объект
                if(map.data.servers61850[index].itemsASDU[indexASDU].length > 0){
                    alert('Тип содержит только один объект');
                    return;
                }
            }
            else if (typeID === '111') {
                //Формирование SVA
                let infElementTempSVA = new InformationElement();
                infElementTempSVA.typeElement = 'SVA';  //SVA
                infElementTempSVA.attributeElement = '';
                Elements.push(infElementTempSVA);
                //Формирование QPM
                let infElementTempQPM = new InformationElement();
                infElementTempQPM.typeElement = 'QPM';  //QPM
                infElementTempQPM.attributeElement = '';
                Elements.push(infElementTempQPM);
                //Поддержка на элементы
                if(sq === true){
                    alert('Тип не поддерживает элементы');
                    return;
                }
                //Одиночный объект
                if(map.data.servers61850[index].itemsASDU[indexASDU].length > 0){
                    alert('Тип содержит только один объект');
                    return;
                }
            }
            else if (typeID === '112') {
                //Формирование IEEE STD 754
                let infElementTempIEEE = new InformationElement();
                infElementTempIEEE.typeElement = 'IEEE STD 754';  //IEEE STD 754
                infElementTempIEEE.attributeElement = '';
                Elements.push(infElementTempIEEE);
                //Формирование QPM
                let infElementTempQPM = new InformationElement();
                infElementTempQPM.typeElement = 'QPM';  //QPM
                infElementTempQPM.attributeElement = '';
                Elements.push(infElementTempQPM);
                //Поддержка на элементы
                if(sq === true){
                    alert('Тип не поддерживает элементы');
                    return;
                }
                //Одиночный объект
                if(map.data.servers61850[index].itemsASDU[indexASDU].length > 0){
                    alert('Тип содержит только один объект');
                    return;
                }
            }
            else if (typeID === '113') {
                //Формирование QPA
                let infElementTempQPA = new InformationElement();
                infElementTempQPA.typeElement = 'QPA';  //QPA
                infElementTempQPA.attributeElement = '';
                Elements.push(infElementTempQPA);
                //Поддержка на элементы
                if(sq === true){
                    alert('Тип не поддерживает элементы');
                    return;
                }
                //Одиночный объект
                if(map.data.servers61850[index].itemsASDU[indexASDU].length > 0){
                    alert('Тип содержит только один объект');
                    return;
                }
            }

            //Проверка на соответствие формату
            if(map.data.servers61850[index].itemsASDU[indexASDU].objectsInf.use){
                if(count !== 0){
                    //Проверка на sq
                    let sqTemp =  map.data.servers61850[index].itemsASDU[indexASDU].objectsInf.sq;
                    if(sq !== sqTemp){
                        alert('Несответствие объектов и элементов');
                        return;
                    }
                    //Проверка на элементы
                    let typeIdTemp = map.data.servers61850[index].itemsASDU[indexASDU].objectsInf.typeID;
                    if(typeID !== typeIdTemp){
                        alert('Несовпадение типа');
                        return;
                    }
                }
            }

            if(sq === false){
                //Добавляем объекты
                if(count === 0){
                    //Заполнение информации о элементах
                    map.data.servers61850[index].itemsASDU[indexASDU].objectsInf.use = true;
                    map.data.servers61850[index].itemsASDU[indexASDU].objectsInf.typeID = typeID;
                    map.data.servers61850[index].itemsASDU[indexASDU].objectsInf.sq = sq;
                }
                let infObj = new InformationObject();
                infObj.addrObj = 0;
                infObj.attributeObj.push(Elements);
                map.data.servers61850[index].itemsASDU[indexASDU].objects.push(infObj);
                //Обновление length
                map.data.servers61850[index].itemsASDU[indexASDU].length = map.data.servers61850[index].itemsASDU[indexASDU].objects.length;
            }
            else if(sq === true){
                //Добавляем элементы
                if(count === 0){
                    let infObj = new InformationObject();
                    infObj.addrObj = 0;
                    infObj.attributeObj.push(Elements);
                    map.data.servers61850[index].itemsASDU[indexASDU].objects.push(infObj);
                    //Заполнение информации о элементах
                    map.data.servers61850[index].itemsASDU[indexASDU].objectsInf.use = true;
                    map.data.servers61850[index].itemsASDU[indexASDU].objectsInf.typeID = typeID;
                    map.data.servers61850[index].itemsASDU[indexASDU].objectsInf.sq = sq;
                }
                else{
                    map.data.servers61850[index].itemsASDU[indexASDU].objects[0].attributeObj.push(Elements);
                }
                //Обновление length
                map.data.servers61850[index].itemsASDU[indexASDU].length = map.data.servers61850[index].itemsASDU[indexASDU].objects[0].attributeObj.length;
            }
        },
        remove_infObj: function (indexObj, indexElem) {
            let index = map.actual.index;
            let indexASDU = this.ShowASDU[index].current;
            let sq = map.data.servers61850[index].itemsASDU[indexASDU].sq;
            if(sq === true){
                //Удаляем элементы
                if(map.data.servers61850[index].itemsASDU[indexASDU].length === 1){
                    map.data.servers61850[index].itemsASDU[indexASDU].objects.splice(0,1);
                    //Обновление length
                    map.data.servers61850[index].itemsASDU[indexASDU].length = 0;
                }
                else{
                    map.data.servers61850[index].itemsASDU[indexASDU].objects[0].attributeObj.splice(indexElem,1);
                    //Обновление length
                    map.data.servers61850[index].itemsASDU[indexASDU].length = map.data.servers61850[index].itemsASDU[indexASDU].objects[0].attributeObj.length;
                }
            }
            else{
                //Удаляем объекты
                map.data.servers61850[index].itemsASDU[indexASDU].objects.splice(indexObj,1);
                //Обновление length
                map.data.servers61850[index].itemsASDU[indexASDU].length = map.data.servers61850[index].itemsASDU[indexASDU].objects.length;
            }
        },
        remove_ASDU: function () {
            let index = map.actual.index;
            let indexASDU = this.ShowASDU[index].current;
            map.data.servers61850[index].itemsASDU.splice(indexASDU, 1);
            map.data.servers61850[index].itemsASDU.sort();
            this.ShowASDU[index].total -= 1;
            this.ShowASDU[index].show = false;
            this.ShowASDU[index].current = 0;
            if(this.ShowASDU[index].total === 0) {
                this.ShowASDU[index].show_left = false;
                this.ShowASDU[index].show_right = false;
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
            TreeView: TreeView,
            ShowASDU: ShowASDU
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
            //Добавляем структуру для оброботки отображения ASDU
            let objASDU = new showASDU();
            ShowASDU.push(objASDU);
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