
import { useLocation } from 'react-router-dom';

function SearchResultsPage() {
    const location = useLocation();
    const { flights } = location.state || { flights: [] };

    return (
        <div>
            <h1>Rezultati pretra�ivanja</h1>
            {flights.length > 0 ? (
                <table>
                    <thead>
                        <tr>
                            <th>Polazni aerodrom</th>
                            <th>Odredi�ni aerodrom</th>
                            <th>Datum polaska</th>
                            <th>Datum povratka</th>
                            <th>Broj putnika</th>
                            <th>Ukupna cijena</th>
                        </tr>
                    </thead>
                    <tbody>
                        {flights.map((flight, index) => (
                            <tr key={index}>
                                <td>{flight.origin}</td>
                                <td>{flight.destination}</td>
                                <td>{flight.departureDate}</td>
                                <td>{flight.returnDate || 'Jednosmjerni'}</td>
                                <td>{flight.passengers}</td>
                                <td>{flight.price} {flight.currency}</td>
                            </tr>
                        ))}
                    </tbody>
                </table>
            ) : (
                <p>Nema rezultata pretra�ivanja.</p>
            )}
        </div>
    );
}

export default SearchResultsPage;
