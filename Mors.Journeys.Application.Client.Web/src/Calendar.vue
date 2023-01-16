<script setup>
import { computed, reactive } from 'vue'

const props = defineProps({ year: Number, month: Number });

const dayNames = ['Mon', 'Tue', 'Wed', 'Thu', 'Fri', 'Sat', 'Sun'];
const longMonthNames = [
    'January',
    'February',
    'March',
    'April',
    'May',
    'June',
    'July',
    'August',
    'September',
    'October',
    'November',
    'December'
];
const dayOfFirst = computed(() => {
    const first = new Date(props.year, props.month - 1, 1);
    return sundayLast(first.getDay());
});

function headerScope() {
    return reactive({
        monthName: computed(() => longMonthNames[props.month - 1]),
        month: props.month,
        year: props.year,
    })
}

function itemScope(row, column) {
    const date = computed(() => {
        var day = (row * 7) + (column + 1) - dayOfFirst.value;
        return new Date(props.year, props.month - 1, day)
    });
    return reactive({
        outside: computed(() => date.value.getMonth() != props.month - 1),
        day: computed(() => date.value.getDate()),
        month: computed(() => date.value.getMonth() + 1),
        year: computed(() => date.value.getFullYear()),
        row,
        column
    });
}

function sundayLast(day) {
    return day == 0 ? 6 : day - 1;
};
</script>

<template>
    <table class="table caption-top">
        <caption>
            <slot name="header" v-bind="headerScope()"></slot>
        </caption>
        <thead>
            <tr>
                <th v-for="dayName in dayNames">{{ dayName }}</th>
            </tr>
        </thead>
        <tbody>
            <tr v-for="row in [0, 1, 2, 3, 4, 5]">
                <td v-for="column in [0, 1, 2, 3, 4, 5, 6]">
                    <slot name="item" v-bind="itemScope(row, column)"></slot>
                </td>
            </tr>
        </tbody>
    </table>
</template>

<style scoped>

</style>