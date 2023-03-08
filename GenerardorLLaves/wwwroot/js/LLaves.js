
function GenerarLlaves() {


    $("#numberOnehexadecimal").empty();
    $("#numberTwohexadecimal").empty();
    $("#numberBinario").empty();


    let data = "1";

    $.ajax({
        url: '/Home/GenerarLlaves',
        dataType: "json",
        type: 'GET',
        data: { data: data },
        contentType: "application/json; charset=utf-8",
        success: function (response) {
                     
            $("#numberOnehexadecimal").val(response.numberOnehexadecimal);
            $("#numberTwohexadecimal").val(response.numberTwohexadecimal);
            $("#numberBinario").val(response.numberBinario);

        }
    });



}