<template>
  <div class="wrapper">
    <div class="pageHeader">
      <div class="searchContainer">
        <SearchRides class="flex-grow-1" @filter-rides="handleFilterRides"/>
        <NewRideForm @handle-refetch="handleRefetch"/>
      </div>
    </div>
    <div class="container">
      <div class="filterContainer">
        <v-col>
          <v-row>
            <SortByComponent/>
          </v-row>
          <v-divider v-if="showRangeFilter" :thickness="5" class="border-opacity-75 rounded my-8"/>
          <v-row>
            <RangeFilterComponent
                v-if="showRangeFilter"
                :max-value="initialPriceRange.upper"
                :min-value="initialPriceRange.lower"
            />
          </v-row>
        </v-col>
      </div>
      <v-divider vertical :thickness="2" class=" border-opacity-50 mx-5 rounded"/>
      <div class="rideContainer">
        <div v-if="showLoading" class="loadingContainer">
          <v-progress-circular indeterminate width="6"/>
        </div>

        <div v-for="ride in filteredRides" :key="ride.id">
          <RidePosting :ride="ride"
             @handle-refetch="handleRefetch"/>
        </div>

        <div v-if="showSnackbar">
          <v-snackbar
              v-model="snackbar"
          >
            No active rides
            <template v-slot:actions>
              <v-btn
                  color="pink"
                  variant="text"
                  @click="retry()"
              >
                Retry
              </v-btn>
            </template>
          </v-snackbar>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import RidePosting from "./components/RidePosting.vue";
import SearchRides from "./components/SearchRides.vue";
import RangeFilterComponent from "./components/RangeFilterComponent.vue";
import SortByComponent from "./components/SortByComponent.vue";
import {useRidesStore} from "./store/rideStore.js";
import {mapActions, mapState} from "pinia";
import NewRideForm from "./components/popups/NewRideForm.vue";

export default {
  components: {
    NewRideForm,
    RidePosting,
    SearchRides,
    RangeFilterComponent,
    SortByComponent
  },

  data() {
    return {
      loading: false,
      snackbar: false,

      filters: {
        from: null,
        to: null,
        date: null,
        seats: null,
      },
    }
  },

  computed: {
    ...mapState(useRidesStore, ['rides', 'initialPriceRange']),

    showSnackbar() {
      this.snackbar = !this.rides.length;
      return this.snackbar;
    },

    showLoading() {
      return this.rides.length === 0;
    },

    showRangeFilter() {
      return this.initialPriceRange.lower && this.initialPriceRange.upper;
    },

    filteredRides() {
      const { from, to, date, seats } = this.filters;

      return this.rides.filter(ride => {
        const rideDepartureDate = new Date(ride.departureDate.split('T')[0]);

        const fromMatch = !from || ride.startLocation === from;
        const toMatch = !to || ride.endLocation === to;
        const dateMatch = !date ||  rideDepartureDate.toLocaleString().split(',')[0] === date.toLocaleString().split(',')[0];
        const seatMatch = !seats || ride.availableSeats >= seats;

        return fromMatch && toMatch && dateMatch && seatMatch && dateMatch;
      })
    }
  },

  async beforeMount() {
    await this.getAllRides();
  },

  methods: {
    ...mapActions(useRidesStore, ['getAllRides', 'updatePriceRangeParams']),

    async retry() {
      this.updatePriceRangeParams(this.initialPriceRange.lower, this.initialPriceRange.upper);
      await this.getAllRides();
    },

    async handleRefetch() {
      await this.getAllRides();
      this.$forceUpdate();
    },

    handleFilterRides(filters) {
      this.filters = Object.assign({}, filters);
    }
  }
}
</script>
<style scoped>
.wrapper {
  display: flex;
  flex-direction: column;
}

.pageHeader {
  display: flex;
  justify-content: center;
  height: 120px;
  width: 100% !important;
  border-bottom: 1px solid white;
  margin-top: 10px;
}


.searchContainer{
  display: flex;
  width: 80%;
}
.container {
  display: flex;
  justify-content: space-between;
  width: 130vh;
  min-height: 90%;

  border: 1px solid darkgray;
  border-radius: 10px;

  padding: 2vh;
  margin: 15px auto;
}

.filterContainer {
  flex-grow: 1;
}

.rideContainer {
  display: flex;
  flex-direction: column;
  flex-grow: 2;
}

.loadingContainer {
  display: flex;
  justify-content: center;
  align-items: center;
  height: calc(100vh - 200px);
}
</style>
