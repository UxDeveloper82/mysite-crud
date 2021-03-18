// Declare Values
const sectionCard = document.querySelector('#card');
const searchBar = document.getElementById('searchBar');
const container = document.querySelector('.btn-container');
const pagination_element = document.getElementById('pagination');

let portCharacters = [];
let current_page = 1;
let rows = 5;


//load items
window.addEventListener('DOMContentLoaded', function (e) {
    displayMenuButtons(portCharacters);
});

//Search Function
searchBar.addEventListener('keyup', (e) => {
    const searchString = e.target.value.toLowerCase();
    const filteredPorts = portsCharacters.filter(portfolios => {
        return portfolios.name.toLowerCase().includes(searchString) ||
               portfolios.type.toLowerCase().includes(searchString);
    });
    displayMenuItems(filteredPorts);
});

// Fetch the items 
const loadPorts = async () => {
    try {
        const resp = await fetch('/api/portfolio');
        portsCharacters = await resp.json();
        displayMenuItems(portsCharacters);
        console.log(portsCharacters);
    } catch (err) {
        console.log(err);
    }
};
loadPorts();

//Display items to screen
const displayMenuItems = (portfolios) => {
    const htmlString = portfolios.map((portfolios) => {
        return ` 
               <div class="col-lg-4 item ${portfolios.type}">
                            <div class="card">
                                <div class="card-head">
                                    <img src="/Image/${portfolios.portfolioPhoto}" alt="" class="img-fluid card-img">
                                    <div class="card-overlay"><h2>${portfolios.name}</h2></div>
                                    <div class="card-hover">
                                        <h2>Language: ${portfolios.language}</h2>
                                        <p>Details: ${portfolios.detail}</p>
                                    </div>
                                </div>
                                <div class="card-body text-center">
                                    <h4 class="title">${portfolios.name}</h4>
                                    <a href="${portfolios.link}" class="btn btn-lg card-btn">Goto Page</a>
                                </div>
                            </div>
                    </div>
               `;
    }).join('');
    sectionCard.innerHTML = htmlString;
};


function displayMenuButtons() {
    const filterBtns = document.querySelectorAll('.filter-btn');
    //filter items
    filterBtns.forEach(function (btn) {
        btn.addEventListener('click', function (e) {
            const type = e.currentTarget.dataset.id;
            const menuType = portsCharacters.filter(function (port) {
                console.log(port.type);
                if (port.type === type) {
                    return port;
                }
            });
            if (type === 'all') {
                displayMenuItems(portsCharacters);
            } else {
                displayMenuItems(menuType);
            }
        })
    })
}
displayMenuButtons();


$(document).ready(function () {
    $('.card').mouseenter(function () {
        $(this).find('.card-overlay').css({ 'top': '-100%' });
        $(this).find('.card-hover').css({ 'top': '0' });
    }).mouseleave(function () {
        $(this).find('.card-overlay').css({ 'top': '0' });
        $(this).find('.card-hover').css({ 'top': '100%' });
    });

})


