Type.registerNamespace("foo.UI");

$foo = foo.UI;


$foo.jsonProxy = function (url) {

    this.url = url;

    this.invoke = function (opts) {

        var o = opts || {};
        var I = this;
        var serviceUrl = I.url;

        $.getJSON(serviceUrl, null, opts.success, opts.fail);
    }


}

$foo.jsonProxy.registerClass("$foo.jsonProxy");

$foo.Mechanic = function (name) {

    this._name = name;
    this._perId = null;
}

$foo.Mechanic.prototype = {

    set_perId: function (perId) {
        this._perId = perId;
    },

    get_perId: function () {
        return this._perId;
    },

    set_name: function (name) {
        this._name = name;
    },

    get_name: function () {
        return this._name;
    }

}


$foo.Mechanic.registerClass("$foo.Mechanic");