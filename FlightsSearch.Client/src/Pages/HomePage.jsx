import { useState } from 'react';
import axios from 'axios';
import { useNavigate } from 'react-router-dom';

function HomePage() {
    const [origin, setOrigin] = useState('');
    const [destination, setDestination] = useState('');
    const [departureDate, setDepartureDate] = useState('');
    const [returnDate, setReturnDate] = useState('');
    const [passengers, setPassengers] = useState(1);

    const navigate = useNavigate();

    const handleSearch = async () => {
        try {
            const response = await axios.post('https://localhost:5001/api/flights/search', {
                origin,
                destination,
                departureDate,
                returnDate,
                passengers,
            });
            navigate('/results', { state: { flights: response.data } });
        } catch (error) {
            console.error('Error fetching flight data:', error);
        }
    };

    return (
        <div>
            <h1>Pretraživanje letova</h1>
            <input type="text" placeholder="Polazni aerodrom" value={origin} onChange={(e) => setOrigin(e.target.value)} />
            <input type="text" placeholder="Odredišni aerodrom" value={destination} onChange={(e) => setDestination(e.target.value)} />
            <input type="date" value={departureDate} onChange={(e) => setDepartureDate(e.target.value)} />
            <input type="date" value={returnDate} onChange={(e) => setReturnDate(e.target.value)} />
            <input type="number" value={passengers} onChange={(e) => setPassengers(e.target.value)} />
            <button onClick={handleSearch}>Pretraži</button>
        </div>
    );
}

export default HomePage;
