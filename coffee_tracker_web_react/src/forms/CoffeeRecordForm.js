import React, { useState } from "react";
import CoffeeTrackerClient from "../api/CoffeeTrackerClient";

export function AddCoffeeRecordForm() {
    const [formData, setFormData] = useState({
        userId: "",
        timeOfConsumption: "",
        coffeeType: "",
        location: "",
    });

    const [message, setMessage] = useState("");

    // Handle form input changes
    const handleChange = (event) => {
        const { name, value } = event.target;
        setFormData((prevData) => ({
            ...prevData,
            [name]: value,
        }));
    };

    // Handle form submission
    const handleSubmit = async (event) => {
        event.preventDefault();

        try {
            await CoffeeTrackerClient.addCoffeeRecord(formData);
            setMessage("Coffee record successfully added!");
            setFormData({
                userId: "",
                timeOfConsumption: "",
                coffeeType: "",
                location: "",
            });
        } catch (error) {
            console.error("Failed to add coffee record:", error);
            setMessage("Error adding coffee record. Please try again.");
        }
    };

    return (
        <div>
            <h2>Add Coffee Record</h2>
            <form onSubmit={handleSubmit}>
                <div>
                    <label htmlFor="userId">User ID:</label>
                    <input
                        type="number"
                        id="userId"
                        name="userId"
                        value={formData.userId}
                        onChange={handleChange}
                        required
                    />
                </div>
                <div>
                    <label htmlFor="timeOfConsumption">Time of Consumption:</label>
                    <input
                        type="datetime-local"
                        id="timeOfConsumption"
                        name="timeOfConsumption"
                        value={formData.timeOfConsumption}
                        onChange={handleChange}
                        required
                    />
                </div>
                <div>
                    <label htmlFor="coffeeType">Coffee Type:</label>
                    <input
                        type="text"
                        id="coffeeType"
                        name="coffeeType"
                        value={formData.coffeeType}
                        onChange={handleChange}
                        required
                    />
                </div>
                <div>
                    <label htmlFor="location">Location:</label>
                    <input
                        type="text"
                        id="location"
                        name="location"
                        value={formData.location}
                        onChange={handleChange}
                        required
                    />
                </div>
                <button type="submit">Add Record</button>
            </form>
            {message && <p>{message}</p>}
        </div>
    );
}
