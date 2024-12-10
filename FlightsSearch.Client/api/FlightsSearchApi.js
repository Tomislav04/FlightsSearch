import axios from 'axios';

const API_URL = 'http://localhost:5045/FlightsSearch/FlightSearch/SearchFlights';

export const searchFlights = async (flightRequest) => {
    try {
        const response = await axios.get(API_URL, {
            params: flightRequest,   // Attach query parameters here
            headers: { "Content-Type": "application/json" }
        });
        return response.data;
    } catch (error) {
        console.error("Error searching flights:", error.response?.data.errors || error.message);
    }
};
