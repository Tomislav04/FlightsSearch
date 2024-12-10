import '../Css/FlightResultsTable.css';
import { useState } from 'react';
import ReactPaginate from 'react-paginate';
import PropTypes from 'prop-types';

function FlightResultsTable({ results, isOneWay }) {
    const itemsPerPage = 5;

    const [currentPage, setCurrentPage] = useState(0);

    const indexOfLastFlight = (currentPage + 1) * itemsPerPage;
    const indexOfFirstFlight = indexOfLastFlight - itemsPerPage;
    const currentFlights = results?.slice(indexOfFirstFlight, indexOfLastFlight);

    const handlePageClick = (event) => {
        setCurrentPage(event.selected);
    };

    if (!results || results.length === 0) {
        return <div><p>Nema dostupnih letova.</p></div>;
    }

    return (
        <div>
            <table>
                <thead>
                    <tr>
                        <th>Departure</th>
                        <th>Destination</th>
                        <th>Date of departure</th>
                        {!isOneWay && <th>Date of return</th>}
                        <th>Stopovers (depart)</th>
                        {!isOneWay && <th> Stopovers (return)</th>}
                        <th>Number of passengers</th>
                        <th>Currency</th>
                        <th>Total price</th>
                    </tr>
                </thead>
                <tbody>
                    {currentFlights.map((flight, index) => (
                        <tr key={index}>
                            <td>{flight.origin}</td>
                            <td>{flight.destination}</td>
                            <td>{flight.departureDate}</td>
                            {!isOneWay && <td>{flight.returnDate}</td>}
                            <td>{flight.stopoversDeparture}</td>
                            {!isOneWay && <td>{flight.stopoversReturn}</td>}
                            <td>{flight.passengersNumber}</td>
                            <td>{flight.currency}</td>
                            <td>{flight.totalPrice}</td>
                        </tr>
                    ))}
                </tbody>
            </table>

            <ReactPaginate
                previousLabel={"← Back"}
                nextLabel={"Next →"}
                pageCount={Math.ceil(results.length / itemsPerPage)}
                onPageChange={handlePageClick}
                containerClassName={"pagination"}
                pageClassName={"page-item"}
                pageLinkClassName={"page-link"}
                activeClassName={"active"}
                disabledClassName={"disabled"}
            />
        </div>
    );
}

FlightResultsTable.propTypes = {
    results: PropTypes.arrayOf(
        PropTypes.shape({
            origin: PropTypes.string.isRequired,
            destination: PropTypes.string.isRequired,
            departureDate: PropTypes.string.isRequired,
            returnDate: PropTypes.string,
            stopoversDeparture: PropTypes.number.isRequired,
            stopoversReturn: PropTypes.number.isRequired,
            returnStops: PropTypes.number.isRequired,
            passengersNumber: PropTypes.number.isRequired,
            currency: PropTypes.string.isRequired,
            totalPrice: PropTypes.number.isRequired,
        })
    ).isRequired,
    isOneWay: PropTypes.bool.isRequired,
};

export default FlightResultsTable;
