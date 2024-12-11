const apiUrl = "http://localhost:5076/";

export default class CoffeeTrackerClient {
    static async callApi(endpoint, method = "GET", body = null) {
        const options = {
            method: method,
            headers: { 'Content-Type': 'application/json' },
        };
        if (body) {
            options.body = JSON.stringify(body);
        }

        try {
            const response = await fetch(apiUrl + endpoint, options);

            return await response.json();
        } catch (error) {
            console.error(`Error calling API: ${method} ${endpoint}`, error);
            throw error;
        }
    }
    
    static async addCoffeeRecord(coffeeRecord) {
        return this.callApi("coffee", "POST", coffeeRecord);
    }
    
    static async getAll() {
        return this.callApi("coffee");
    }
    
    static async getById(id) {
        return this.callApi(`coffee/${id}`);
    }
}
