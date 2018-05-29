function renderThumbnails(preCategory) {

    if ($('select[name=AllCategories]').val() != null) {
        preCategory = $('select[name=AllCategories]').val();
    }

    console.log(preCategory);

    $.ajax({
        type: 'GET',
        url: "/Stories/SelectedThumbnails/", 
        data: {
            category: preCategory  //Data need to pass as parameter                       
        },
        dataType: 'html', //dataType - html
        success: function (result) {
            //Create a Div around the Partial View and fill the result
            $('#stories-gallery').html(result);
        }
    });

}

function renderLoginSpan() {
    $.ajax({
        type: 'GET',
        url: "/User/LoginSpan/",
        dataType: 'html', //dataType - html
        success: function (result) {
            //Create a Div around the Partial View and fill the result
            $('#LoginSpan').html(result);
        }
    });
}

/*
$('#login').submit(function() {

    $.ajax({
        type: 'GET',
        url: "/User/LoginSpan/",
        dataType: 'html', //dataType - html
        success: function (result) {
            //Create a Div around the Partial View and fill the result
            $('#LoginSpan').html(result);
        }
    });
    return false;

})
*/

//carousel

var reachedEnd = false;
var crossEnd = false;

function nextSlide() {
	clearInterval(newTimer);
	newTimer = setInterval(timeClick, 6000);

	var slide1 = document.getElementsByClassName('slide-active')[0];
	var slide2 = document.getElementsByClassName('slide-active')[1];
	var sibling = slide2.nextElementSibling;


	if (sibling == null) {
		reachedEnd = true;
		sibling = slide2.parentNode.firstElementChild;
	}

	if(crossEnd){
		slide1.classList.remove('floatright');
		crossEnd = false;
		reachedEnd = false;

		slide2.classList.remove('slide-active');
		slide2.classList.add('hidden');

		sibling = slide1.nextElementSibling;

		sibling.classList.add('slide-active');
		sibling.classList.remove('hidden');
		return false;
	}

	if(reachedEnd){
		sibling.classList.add('floatright');
		crossEnd = true;
	}


	slide1.classList.remove('slide-active');
	slide1.classList.add('hidden');

	sibling.classList.add('slide-active');
	sibling.classList.remove('hidden');
}

function prevSlide() {
	clearInterval(newTimer);
	newTimer = setInterval(timeClick, 6000);

	var slide1 = document.getElementsByClassName('slide-active')[0];
	var slide2 = document.getElementsByClassName('slide-active')[1];
	var sibling = slide1.previousElementSibling;

	console.log(slide1);
	console.log(slide2);


	if (sibling == null) {
		reachedEnd = true;
		sibling = slide1.parentNode.lastElementChild;
		console.log(sibling);
	}

	if(crossEnd){
		slide1.classList.remove('floatright');
		crossEnd = false;
		reachedEnd = false;

		slide1.classList.remove('slide-active');
		slide1.classList.add('hidden');

		sibling = slide2.previousElementSibling;

		sibling.classList.add('slide-active');
		sibling.classList.remove('hidden');
		return false;
	}

	if(reachedEnd){
		slide1.classList.add('floatright');
		crossEnd = true;
	}


	slide2.classList.remove('slide-active');
	slide2.classList.add('hidden');

	sibling.classList.add('slide-active');
	sibling.classList.remove('hidden');
	
}

var next = document.getElementById('next');
if(next != null){
	next.addEventListener('click', nextSlide);
}

var prev = document.getElementById('prev');
if(prev != null){
	prev.addEventListener('click', prevSlide);
}


if(prev != null && next != null){
	function timeClick() {
	next.click();
	}
	var newTimer = setInterval(timeClick, 6000);
}

var select = document.getElementById('AllCategories');
if (select != null) {
    var option = $('#AllCategories').find('option')[0];
    option.setAttribute('disabled', true);
}
