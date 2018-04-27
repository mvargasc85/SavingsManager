﻿var savingsObservable = null;
$(document).ready(function () {

    savingsObservable = GetSavingsObserver();

    if ($("#savingsAction") && $("#savingsAction").val() != undefined && $("#savingsAction").val() != '') {
        var SavingsAction = $("#savingsAction").val();
        savingsObservable.fire('Socio', SavingsAction);
    }

});


function saveSocio() {

    var socioModel = getSocioModel();
    $.ajax({
        type: "Post",
        url: "/Socio/CrearSocio",
        data: socioModel,
        success: function (result) {
            var url = "/Socio/VerSocios";
            window.location.href = url;
        },
        error: function (e) { display(e); }
    });
}


function getSocioModel() {
    var socioModel =
        {
            IdSocio: null,
            Nombre: $("#socioNombreTxt").val(),
            Apellido1: $("#socioApellido1Txt").val(),
            Apellido2: $("#socioApellido2Txt").val(),
            Email: $("#socioEmailTxt").val(),
            IdGrupo: dropDownListObject("grupoDropDown").value()
        };

    return socioModel;
}


function cancelAddingSocio() {
    $("#socioNombreTxt").val("");
    $("#socioApellido1Txt").val("");
    $("#socioApellido2Txt").val("");
    $("#socioEmailTxt").val("");
}




function showGroupsCombo(result) {

    if (result !== "") {
        createGroupsGrid("groupsGrid", result);
        $("#groupsDiv").show();
    } else
        $("#groupsDiv").hide();

}

function getAllSocios() {
    $.ajax({
        type: "get",
        url: "/Socio/GetSocios",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        //  async: true,
        success: function (result) {
            showSociosGrid(result);
        },
        error: function (e) {
            display(e);
        }
    });
}


function showSociosGrid(result) {

    if (result !== "") {
        createSociosGrid("sociosGrid", result);
        $("#sociosDiv").show();
        //var url = "/Home/VerGrupos";
        //window.location.href = url;
    } else
        $("#sociosDiv").hide();

}

function createSociosGrid(divId, items) {
    $("#" + divId).kendoGrid({
        dataSource: {
            data: items,
            schema: {
                model: {
                    fields: {
                        IdSocio: { type: "number" },
                        Nombre: { type: "string" },
                        Apellido1: { type: "string" },
                        Apellido2: { type: "string" },
                        Email: { type: "string" },
                        IdGrupo: { type: "number" }
                    }
                }
            },

            pageSize: 5
        },
        //        height: 100,
        scrollable: false,
        sortable: true,
        filterable: true,
        columns: [
            { field: "Nombre", title: "Nombre", width: "50px" },
            { field: "Apellido1", title: "Apellido1", width: "50px" },
            { field: "Apellido2", title: "Apellido2", width: "50px" },
            { field: "Email", title: "Email", width: "50px" },
            { field: "IdGrupo", title: "IdGrupo", width: "50px" },
            {
                template: '<a href="javascript:void(0)" class="k-grid-edit" onclick="EditSocio(${IdSocio})">Editar</a>&nbsp;&nbsp;&nbsp;|&nbsp;&nbsp;' +
                '<a href="javascript:void(0)" class="k-grid-delete" onclick="ConfirmDeleteSocio(${IdSocio})">Eliminar</a>',
                width: "40px", attributes: { style: "text-align:center;" }
            }
        ]
    });
}

function EditSocio(idSocio) {
    var url = "/Socio/EditarSocio?idSocio=" + idSocio;
    window.location.href = url;
}
function ConfirmDeleteSocio(idSocio) {
    $("#delete-socio-dialog-confirm").dialog({
        resizable: false,
        height: "auto",
        width: 400,
        modal: true,
        buttons: {
            "Eliminar": function () {
                deleteSocio(idSocio);
            },
            "Cancelar": function () {
                $(this).dialog("close");
            }
        }
    });
}

function deleteSocio(idSocio) {
    $.ajax({
        type: "Post",
        url: "/Socio/EliminarSocio?idSocio=" + idSocio,
        success: function (result) {
            var url = "/Socio/VerSocios";
            window.location.href = url;
        },
        error: function (e) { display(e); }
    });
}

function display(e) { alert(e); }

function getSocioDropDownList() {
    var socioId = $("#socioDropDown").val();
    $.ajax({
        url: "/Socio/GetSocios",
        type: "get",
        datatype: "json",
        contentType: "application/json; charset=utf-8",
        success: function (result) {
            CreateDropDownlist("socioDropDown", $.parseJSON(result), "Nombre", "IdSocio", null, "Seleccione ...", null);

        },
        error: function () { alert("Problema al cargar los socios") }
    });
}



function showSociosCombo(result) {

    if (result !== "") {
        createSociosGrid("sociosGrid", result);
        $("#sociosDiv").show();
    } else
        $("#sociosDiv").hide();

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

    var ddl = dropDownListObject(divId);
    if (ddl != null)
        ddl.select(0);
}


var dropDownListObject = (function (ddlId) {
    return $('#' + ddlId).data("kendoDropDownList");
});



var Socio = function () { }

Socio.prototype = {
    initializeBtns: function () {
        $("#AddSocioBtn").on("click", saveSocio);
        $("#cancelAddingSocioBtn").on("click", cancelAddingSocio);
    },

    loadSociosGrid: function () {
        getAllSocios();
    },

    loadSocioDropDownList: function () {
        getSocioDropDownList();
    }
}



