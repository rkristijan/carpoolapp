<template>
  <div class="card">
    <div class="cardHeader">
      <div class="timeOriginSection">
        <div class="rideOrigin">
          <span class="origin-city text-subtitle-1">{{ ride.startLocation }}</span>
          <div class="line"></div>
          <span class="destination-city text-subtitle-1">{{ ride.endLocation }}</span>
        </div>

        <div class="rideTimes">
            <span class="departure-time text-button">
              <v-icon icon="mdi-calendar-range" size="large"/>
              {{ getDepartureDate }}
            </span>
        </div>
      </div>
      <div class="ridePrice">
        <v-icon icon="mdi-currency-usd" size="large"/>
        <span class="price text-h5 font-weight-bold">
          {{ ride.price }}
        </span>
      </div>
    </div>

    <v-divider class="border-opacity-50"/>

    <div class="cardFooter">
      <div class="rideDetails">
            <span class="driverName text-subtitle-1 font-weight-bold">
              <v-icon icon="mdi-account-tie" size="large"/>
              {{ ride.driverName }}
            </span>
        <span class="seating">
              <v-icon v-for="i in getSeatNumber"
                      :color=" i <= getAvailableSeats ? 'green' : 'black' "
                      :icon=" i <= getAvailableSeats ? 'mdi-account' : 'mdi-account-outline' "
              />
            </span>
      </div>
      <div class="footerButtons">
        <v-btn
            class="mr-2"
            density="comfortable"
            color="red"
            :disabled="getAvailableSeats === getSeatNumber"
            @click="handleCancel"
        >
          Cancel
        </v-btn>

        <v-btn variant="outlined"
               density="comfortable"
               :disabled="getAvailableSeats === 0"
               @click="handleBooking"
        >
          Book
        </v-btn>
        <v-btn
            icon="mdi-delete-outline"
            variant="plain" color="red"
            density="comfortable"
            class="ml-2"
            @click="handleDelete"
        />
      </div>
    </div>
  </div>
</template>

<script>
import {bookSeat, cancelSeat, deleteRide} from "../services/api/rides.js";

import {useRidesStore} from "../store/rideStore.js";
import {mapActions} from "pinia";

export default {
  name: 'TravelComponent',

  props: {
    ride: {
      type: Object,
      required: true
    }
  },

  computed: {
    getSeatNumber() {
      return this.ride.vehicleSeats;
    },

    getAvailableSeats() {
      return this.ride.availableSeats;
    },

    getDepartureDate() {
      return new Date(this.ride.departureDate).toLocaleString().split(',')[0];
    }
  },

  methods: {
    ...mapActions(useRidesStore, ['getAllRides']),

    async handleBooking() {
      await bookSeat(this.ride.id);
      this.$emit('handle-refetch');
    },

    async handleCancel() {
      await cancelSeat(this.ride.id);
      this.$emit('handle-refetch');
    },

    async handleDelete(){
      await deleteRide(this.ride.id);
      this.$emit('handle-refetch')
    }
  }
}
</script>

<style scoped>

.rideOrigin {
  display: flex;
}

.line {
  margin: 12px 4px 0 4px;
  border-top: 3px solid black;
  width: 60%;
  position: relative;
}

.line:before, .line:after {
  content: "";
  width: 15px;
  height: 15px;
  background: #ddd;
  border: 3px solid black;
  border-radius: 10px;
  position: absolute;
  top: -8px;
}

.line:after {
  right: 0;
}

.cardHeader {
  display: flex;
  justify-content: space-between;
  padding: 16px;
}

.cardFooter {
  display: flex;
  justify-content: space-between;
  padding: 16px;
}

.timeOriginSection {
  display: flex;
  flex-direction: column;

  flex-basis: 50%;
}

.card {
  display: flex;
  flex-direction: column;
  justify-content: space-between;
  color: black;
  background: #ddd;
  border: 1px solid black;
  border-radius: 4px;
  margin-bottom: 16px;
}

.rideDetails {
  flex-grow: 1;
  display: flex;
  justify-content: space-between;
  align-items: center;
  margin-bottom: 8px;
  margin-right: 30px;
}

.rideTimes {
  display: flex;
  justify-content: space-between;
  margin-top: 8px;
}

.ridePrice{
  display: flex;
}

.footerButtons {
  display: flex;
  flex-grow: 2;
  justify-content: flex-end;
}
</style>