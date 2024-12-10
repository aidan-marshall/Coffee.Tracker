const apiUrl = "http://localhost:5076/";

function CallApi(endpoint) {
    return fetch(apiUrl + endpoint, {
        method: 'GET',
        headers: { 'Content-Type': 'application/json' },
    })
        .then(response => {
            if (!response.ok) throw new Error("Failed to fetch");
            return response.json();
        })
        .catch(err => console.error(err));
}

export function GetAll () {
    return CallApi("coffee");
}
