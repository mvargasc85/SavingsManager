$(document).ready(function () {
    $("#Menu1").kendoMenu();
    $("#AddGroupBtn").on("click", saveGroup);
    $("#AddSocioBtn").on("click", saveSocio);
    $("#cancelAddingGroupBtn").on("click", cancelAddingGroup);
    $("#cancelAddingSocioBtn").on("click", cancelAddingSocio);
    getAllGroups();
    getAllSocios();
    getGroupList();
});

//groups methods
function saveGroup() {
    
        var groupModel = getGroupModel();
        $.ajax({
            type: "Post",
            url: "/Home/CrearGrupo",
            data: groupModel,
            success: function (result) {
                var url = "/Home/VerGrupos";
                window.location.href = url;
            },
            error: function (e) { display(e); }
        });
}

function saveSocio() {

    var socioModel = getSocioModel();
    $.ajax({
        type: "Post",
        url: "/Home/CrearSocio",
        data: socioModel,
        success: function (result) {
            var url = "/Home/VerSocios";
            window.location.href = url;
        },
        error: function (e) { display(e); }
    });
}

function getGroupModel() {
    var groupModel =
    {
        IdGrupo: null,
        Nombre: $("#groupNameTxt").val(),
        Descripcion: $("#groupDescriptionTxt").val()
    };
    
    return groupModel;
}

function getSocioModel() {
    var socioModel =
        {
            IdSocio: null,
            Nombre: $("#socioNombreTxt").val(),
            Apellido1: $("#socioApellido1Txt").val(),
            Apellido2: $("#socioApellido2Txt").val(),
            Email: $("#socioEmailTxt").val(),
            IdGrupo: $("#grupoDropDown").val()
        };

    return socioModel;
}

function cancelAddingGroup() {
    $("#groupNameTxt").val("");
    $("#groupDescriptionTxt").val("");
}

function cancelAddingSocio() {
    $("#socioNombreTxt").val("");
    $("#socioApellido1Txt").val("");
    $("#socioApellido2Txt").val("");
    $("#socioEmailTxt").val("");
}


function getAllGroups() {
    $.ajax({
        type: "get",
        url: "/Home/GetGrupos",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        //  async: true,
        success: function(result) {
             showGroupsGrid(result);
        },
        error: function(e) {
             display(e);
        }
    });
}

function getGroupList() {
    debugger;
    var grupoId = $("#grupoDropDown").val();
    $.ajax({
        url: "/Home/GetGrupos",
        type: "get",
        datatype: "json",
        contentType: "application/json; charset=utf-8",
        //data: JSON.stringify({ groupId: + groupId }),
        success: function (result) {
            $("#grupoDropDown").html("");
            $("#grupoDropDown").append
                ($('<option></option>').val(null).html("---Selecione el grupo---"));
            $.each($.parseJSON(result), function (i, grupo) { $("#grupoDropDown").append($('<option></option>').val(grupo.IdGrupo).html(grupo.Nombre)) })

        },
        error: function () { alert("Problema al cargar los grupos") },
    });
}

function showGroupsCombo(result) {

    if (result !== "") {
        createGroupsGrid("groupsGrid", result);
        $("#groupsDiv").show();
        //var url = "/Home/VerGrupos";
        //window.location.href = url;
    } else
        $("#groupsDiv").hide();

}

function getAllSocios() {
    $.ajax({
        type: "get",
        url: "/Home/GetSocios",
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

function showGroupsGrid(result) {

    if (result !== "") {
        createGroupsGrid("groupsGrid", result);
        $("#groupsDiv").show();
        //var url = "/Home/VerGrupos";
        //window.location.href = url;
    } else
        $("#groupsDiv").hide();

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

function createGroupsGrid(divId, items) {
    $("#" + divId).kendoGrid({
        dataSource: {
            data: items,
            schema: {
                model: {
                    fields: {
                        IdGrupo: { type: "number" },
                        Nombre: { type: "string" },
                        Descripcion: { type: "string" },
                        FechaCreacion: { type: "date" }
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
            { field: "Descripcion", title: "Descripcion", width: "50px" },
            { field: "FechaCreacion", title: "Fecha de Creación", width: "50px", format: "{0: dd/MM/yyyy}" },
            {
                template: '<a href="javascript:void(0)" class="k-grid-edit" onclick="EditGroup(${IdGrupo})">Editar</a>&nbsp;&nbsp;&nbsp;|&nbsp;&nbsp;' +
                    '<a href="javascript:void(0)" class="k-grid-delete" onclick="ConfirmDeleteGroup(${IdGrupo})">Eliminar</a>',
                width: "40px", attributes: { style: "text-align:center;" }
            }
           
        ]
    });
}


function EditGroup(idGrupo) {
    $.ajax({
        type: "get",
        url: "/Home/GetGroupById?idGrupo=" + idGrupo,
        success: function (result) {
            var url = "/Home/EditGrupos";
            window.location.href = url;
        },
        error: function (e) { display(e); }
    });
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
                template: '<a href="javascript:void(0)" class="k-grid-edit" onclick="alert2(${IdSocio})">Editar</a>&nbsp;&nbsp;&nbsp;|&nbsp;&nbsp;' +
                '<a href="javascript:void(0)" class="k-grid-delete" onclick="ConfirmDeleteSocio(${IdSocio})">Eliminar</a>',
                width: "40px", attributes: { style: "text-align:center;" }
            }

        ]
    });
}


function ConfirmDeleteGroup(idGrupo) {
 $("#delete-group-dialog-confirm").dialog({
        resizable: false,
        height: "auto",
        width: 400,
        modal: true,
        buttons: {
            "Eliminar": function () {
                deleteGroup(idGrupo);
            },
            "Cancelar": function () {
                $(this).dialog("close");
            }
        }
    });
}

function  deleteGroup(idGrupo) {
       $.ajax({
        type: "Post",
        url: "/Home/EliminarGrupo?idGrupo="+idGrupo,
        success: function (result) {
            var url = "/Home/VerGrupos";
            window.location.href = url;
        },
        error: function (e) { display(e); }
    });
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
        url: "/Home/EliminarSocio?idSocio=" + idSocio,
        success: function (result) {
            var url = "/Home/VerSocios";
            window.location.href = url;
        },
        error: function (e) { display(e); }
    });
}

function display(e) { alert(e); }


