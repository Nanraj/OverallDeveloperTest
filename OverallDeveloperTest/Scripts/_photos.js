function display(stock,originalURL) {
	var modal = $('#divimage');
	modal.css('display', 'block');
	$('#modalimg').attr("src", originalURL);
	$("#caption").html(stock.alt);
	modal.click(function () {
		modal.css('display', 'none');
	});
}