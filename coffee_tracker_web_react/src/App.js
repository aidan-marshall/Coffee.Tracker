import React, { useState, useEffect } from "react";
import { createBrowserRouter, RouterProvider } from "react-router-dom";
import { WebSocketContext } from "./WebSocketContext";
import { HubConnectionBuilder } from "@microsoft/signalr";
import { HomePage } from "./pages/HomePage";
import { AddCoffeeRecordForm } from "./forms/CoffeeRecordForm";
import NotFoundPage from "./pages/NotFoundPage";

const createWebSocketConnection = () => {
    const socket = new HubConnectionBuilder()
        .withUrl("http://localhost:5076/coffeeHub", { withCredentials: true })
        .configureLogging("debug")
        .withAutomaticReconnect()
        .build();

    socket
        .start()
        .then(() => {
            console.log("Connected to CoffeeHub");
        })
        .catch((err) => {
            console.error("Error starting WebSocket connection:", err);
        });

    return socket;
};

function App() {
    const [webSocket, setWebSocket] = useState(null);

    useEffect(() => {
        // Create WebSocket connection on component mount
        const socket = createWebSocketConnection();
        setWebSocket(socket);

        return () => {
            // Clean up the connection when the component unmounts
            if (socket) {
                socket.stop().then(() => {
                    console.log("Disconnected from CoffeeHub");
                });
            }
        };
    }, []);

    const router = createBrowserRouter([
        {
            path: "/",
            element: <HomePage />,
            errorElement: <NotFoundPage />,
        },
        {
            path: "/add",
            element: <AddCoffeeRecordForm />,
        },
    ]);

    if (!webSocket) {
        // Render loading state until the WebSocket connection is ready
        return <div>Loading WebSocket...</div>;
    }

    return (
        <WebSocketContext.Provider value={webSocket}>
            <RouterProvider router={router} />
        </WebSocketContext.Provider>
    );
}

export default App;
