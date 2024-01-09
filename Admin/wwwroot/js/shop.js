const searchInput = document.querySelector('.search')
const searchResult = document.querySelector('.search-result')

let cachedInput;

searchInput.addEventListener('keyup', (e) => {
    const value = e.target.value.trim();
    if (value.length < 3) {
        searchResult.innerHTML = ""
        return
    }

    if (cachedInput == value) return

    cachedInput = value

    fetch(`https://localhost:7199/Shop/search?name=${value}`)
        .then(x => x.text())
        .then(x => searchResult.innerHTML = x)
})

