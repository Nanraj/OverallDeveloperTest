//Retrieve Photo from Instagram
function searchInstagramPhoto() {
    var Id = $("#LocationId").val();
    if (Id == "") {
        alert("Please select a Location.")
    }
    else {
        $("#bodyContent").html('');
        $.ajax({
            url: "InstagramPhoto/Search",
            data: { locationId: Id },
            type: 'GET' ,
            success: function (result) {

                $("#bodyContent").html(result);
            }
        });
    }
};