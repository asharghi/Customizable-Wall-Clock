<template>
  <div>
    <div
      v-for="setting in rows"
      :key="setting.name"
      class="settings-row settings-font darker"
      @click="$emit('selected', setting.label)"
    >
      {{setting.label}}
      <div class="edit-icon"></div>
    </div>
  </div>
</template>

<script>
export default {
  props: {
    rows: {
      type: Array,
      required: true
    },
    clockSettings: {
      type: Object,
      required: true
    }
  }
};
</script>