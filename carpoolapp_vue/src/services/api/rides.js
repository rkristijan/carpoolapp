import axios from "axios";

export async function getRidesHelper() {
    return await axios.get('https://localhost:7125/Rides');
}

export async function createRideHelper(rideData) {
    return await axios.post(`https://localhost:7125/Rides/add`, {
        driverName: rideData.driverName,
        startLocation: rideData.startLocation,
        endLocation: rideData.endLocation,
        availableSeats: rideData.availableSeats,
        vehicleSeats: rideData.vehicleSeats,
        price: parseFloat(rideData.price),
        departureDate: rideData.departureDate.toISOString(),
    })
}

export async function bookSeat(id) {
    return await axios.put(`https://localhost:7125/Rides/${id}/book`);
}

export async function cancelSeat(id) {
    return await axios.put(`https://localhost:7125/Rides/${id}/cancel`);
}

export async function deleteRide(id) {
    return await axios.delete(`https://localhost:7125/Rides/${id}`);
}
