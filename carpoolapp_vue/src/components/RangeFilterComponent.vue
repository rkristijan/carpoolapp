<template>
  <v-card variant="tonal" class="filterCard">
    <template v-slot:title>
      <v-row>
        <v-col>
          <p class="text-lg-h5 font-weight-bold">Price range</p>
        </v-col>
        <v-col class="resetButton">
          <v-btn v-if="showReset" variant="text" color="primary" @click="resetSlider">
            Reset
          </v-btn>
        </v-col>
      </v-row>
    </template>

    <template v-slot:text>
      <v-range-slider
          v-model="sliderRange"
          step="1"
          min="0"
          :max="$props.maxValue"
          thumb-label="always"
          class="rangeSlider"
          @end="handleSliderEnd"
      />
    </template>
  </v-card>
</template>

<script>
import {useRidesStore} from "../store/rideStore.js";
import {mapActions} from "pinia";
export default {
  name: 'RangeFilter',

  props: {
    minValue: {
      type: Number,
      required: true
    },
    maxValue: {
      type: Number,
      required: true
    },
  },

  data() {
    return {
      sliderRange: []
    }
  },

  computed: {
    showReset() {
      return this.sliderRange[0] !== this.$props.minValue || this.sliderRange[1] !== this.$props.maxValue;
    }
  },

  beforeMount() {
    this.sliderRange = [this.$props.minValue, this.$props.maxValue];
  },

  methods: {
    ...mapActions(useRidesStore, ['updatePriceRangeParams']),

    resetSlider() {
      if (this.showReset) {
        this.sliderRange = [this.$props.minValue, this.$props.maxValue];
        this.updatePriceRangeParams(this.sliderRange[0], this.sliderRange[1]);
      }
    },

    handleSliderEnd(){
      this.updatePriceRangeParams(this.sliderRange[0], this.sliderRange[1]);
    }
  }
}
</script>
<style scoped>

.filterCard {
  width: 100%;
}

.rangeSlider {
  margin-top: 20px;
  margin-bottom: -20px;
}

.resetButton {
  display: flex;
  justify-content: end;
  padding: 10px;
}
</style>
