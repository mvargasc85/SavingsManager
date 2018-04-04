$(document).ready(function () {
    $("#Menu1").kendoMenu();
    $("#AddGroupBtn").on("click", saveGroup);
    $("#cancelAddingGroupBtn").on("click", cancelAddingGroup);
    getAllGroups();
});


