<template>
  <v-card variant="tonal" class="filterCard">
    <template v-slot:title>
      <v-row>
        <v-col>
          <p class="text-lg-h5 font-weight-bold">Sort by</p>
        </v-col>
        <v-col class="switch">
          <v-switch
              v-model="ascendingValue"
              :label=switchLabel
              color="success"
              hide-details
          />
        </v-col>
      </v-row>
    </template>
    <template v-slot:text>
      <v-radio-group v-model="sortByValue" class="radioGroup">
        <v-radio label="Price" value="price"></v-radio>
        <v-radio label="Departure date" value="departureDate"></v-radio>
        <v-radio label="Available seats" value="availableSeats"></v-radio>
        <v-radio label="Creation date" value="creationDate"></v-radio>
      </v-radio-group>
      <v-btn v-if="showClearAll" variant="text" color="primary" @click="clearSortBy">
        Clear all
      </v-btn>
    </template>
  </v-card>

</template>

<script>

import {mapActions, mapState} from "pinia";
import {useRidesStore} from "../store/rideStore.js";

export default {
  name: 'SortBy',

  data() {
    return {
      sortByValue: null,
      ascendingValue: false,
    }
  },

  watch: {
    sortByValue() {
      this.updateSortParams(this.sortByValue, this.ascendingValue);
    },
    ascendingValue() {
      this.updateSortParams(this.sortByValue, this.ascendingValue);
    },
  },

  async beforeMount() {
    this.sortByValue = "creationDate"
    await this.updateSortParams(this.sortByValue, this.ascendingValue)
  },

  computed: {
    ...mapState(useRidesStore, ['sortBy', 'ascending']),

    switchLabel() {
      return this.ascending ? "Ascending" : "Descending";
    },

    showClearAll() {
      return Boolean(this.sortByValue);
    }
  },

  methods: {
    ...mapActions(useRidesStore, ['sortRides', 'updateSortParams']),

    clearSortBy() {
      this.sortByValue = null;
    },
  }
}
</script>
<style scoped>

.filterCard {
  width: 100%;
}

.switch {
  display: flex;
  justify-content: end;
  padding: 0 1rem 0 0;
}

.radioGroup {
  margin-bottom: -1.5rem;
}

</style>
