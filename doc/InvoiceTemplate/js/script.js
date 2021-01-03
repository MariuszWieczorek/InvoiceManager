document.title = 'Nowy tytuł - JAVASCRIPT';


let links = document.getElementsByClassName('link-blog');
//zwróci nam listę elementów
//możemy dobrać się elementu tej listy przez indeks
links[0].style.fontSize = '44px';

//w pętli możemy zamienić wszystkie elementy danej klasy 
for (let link of links) 
    link.style.fontSize = '44px';

function run() {
    alert('ok');
    updateHeader();
}

function updateHeader() {
    let header = document.getElementById('main-header');
    header.innerHTML = 'zmieniono header przez javascript';
}

// zamiast $ możemy zapisać jQuery
$(document).ready(function () {
    //alert('loaded');
    $('#main-header').prop('innerHTML', 'jQuery 123 ...');
    $('.title_one').css('background-color', 'green');
});