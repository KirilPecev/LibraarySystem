function confirmThisForLibrarian(parameters) {
    confirm("Your librarian has to be registered!");
}

$("#target").keyup(function () {
    $("button").prop("disabled", !this.value);
});

function confirmThisForOwner(parameters) {
    confirm("Owner has to be registered!");
}