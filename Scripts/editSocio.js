$(document).ready(function () {
    $("#Menu1").kendoMenu();
    $("#AddSocioBtn").on("click", saveSocio);
    $("#cancelAddingSocioBtn").on("click", cancelAddingSocio);
    getGroupDropDownList();
});

function loadSocioInfo() {
    //$.ajax({
    //    type: "get",
    //    url: "/Home/GetGroupById?idGrupo=" + idGrupo,
    //    success: function (result) {
    //        var url = "/Home/EditarGrupo";
    //        window.location.href = url;
    //    },
    //    error: function (e) { display(e); }
    //});
}
function getGroupDropDownList() {
    debugger;
    var grupoId = $("#grupoDropDown").val();
    $.ajax({
        url: "/Group/GetGrupos",
        type: "get",
        datatype: "json",
        contentType: "application/json; charset=utf-8",
        success: function (result) {
            CreateDropDownlist("grupoDropDown", $.parseJSON(result), "Nombre", "IdGrupo", null, "Seleccione ...", null);
        },
        error: function () { alert("Problema al cargar los grupos") },
    });
}
function CreateDropDownlist(divId, items, text, value, onchanceEventHandler, selectPlaceHolder, dataBoundEvent) {

    $("#" + divId).kendoDropDownList({
        optionLabel: selectPlaceHolder,
        dataTextField: text,
        dataValueField: value,
        dataSource: items,
        index: 0,
        change: onchanceEventHandler,
        open: dataBoundEvent
    });

    dropDownListObject(divId).select(0);
}


var dropDownListObject = (function (ddlId) {
    return $('#' + ddlId).data("kendoDropDownList");
});

