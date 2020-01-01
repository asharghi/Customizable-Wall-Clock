<template>
  <div class="image-picker">
    <div
      class="image-picker-row"
      v-for="(filenames, index) in filenameGroups"
      :key="'filenameGroup'+index"
    >
      <div
        v-for="filename in filenames"
        :key="filename + '-thumb'"
        :class="{'image-container': true, 'selected': backgroundPath(filename) === selectedPath}"
        @click="$emit('selected', backgroundPath(filename))"
      >
        <img class="image" :src="thumbnailPath(filename)" />
      </div>
    </div>
  </div>
</template>

<script>
export default {
  props: {
    selectedPath: {
      type: String,
      default: ''
    }
  },
  data() {
    return {
      filenameGroups: [
        [1, 2, 3, 4],
        [5, 6, 7, 8],
        [9, 10, 11, 12],
        [13, 14, 15, 16],
        [17, 18, 19, 20],
        [21, 22, 23, 24]
      ]
    };
  },
  methods: {
    backgroundPath(filename) {
      return "/backgrounds/" + filename + ".png";
    },
    thumbnailPath(filename) {
      return "/backgrounds/thumbs/" + filename + ".png";
    }
  }
};
</script>
<style scoped>
.image-picker {
  background: #ffffff;
  padding: 5px;
}
.image-picker-row {
  display: flex;
  align-items: center;
  justify-content: space-between;
  line-height: 0;
  cursor: pointer;
}
.image {
  width: 100%;
}
.image-container {
  width: 25%;
  padding: 5px;
}
.image-container.selected {
  background: #2ecc71;
}
</style>