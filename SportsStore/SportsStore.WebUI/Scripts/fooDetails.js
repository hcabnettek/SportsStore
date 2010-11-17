var person = null;
var other = null;

$(function () {

    LoadData();

})


function LoadData() {

    debugger;

    var props = { id: "user1", name: "Joe", perId: 35 };
    person = $create("$foo.Mechanic", props, null, null, null);


    $.getJSON(serviceUrl, null, 
    
        function (data, context) {

            var props = { id: "user1", name: data.name, perId: data.perId };
            person = $create("$foo.Mechanic", props, null, null, null);
        },

        function (data, context) {

            alert("shoot");
        });

   

}


