<script setup>
import { ref } from 'vue';
import PassengerList from './PassengerList.vue';
import JourneyCalendar from './JourneyCalendar.vue'

const passenger = ref({ name: '', id: null });
</script>

<template>
  <h1>Journeys</h1>
  <div id="main" class="d-flex">
    <JourneyCalendar id="calendar" class="flex-grow-1" :passenger="passenger" />
    <PassengerList id="passengers" @passenger-selected="(x) => passenger = x" />
  </div>
</template>

<style scoped>
#calendar {
  background-color: aliceblue
}

#passengers {
  background-color: beige
}
</style>
