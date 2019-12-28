<template>
  <div class="actionbar">
    <div v-show="previous" class="back-icon" @click="$emit('back')"></div>
    <div v-show="previous" class="previous settings-font" @click="$emit('back')">{{previous}}</div>
    <div class="current settings-font">{{current}}</div>
  </div>
</template>

<script>
export default {
  props: {
    previous: {
      type: String,
      default: ""
    },
    current: {
      type: String,
      default: ""
    }
  }
};
</script>
<style scoped>
.actionbar {
  background: #fbfbfd;
  width: 100%;
  display: flex;
  align-items: center;
  padding-top: 15px;
  padding-bottom: 15px;
  height: 10px;
  border-bottom: 1px solid #d8d8d8;
}
.previous {
  color: #0a84ff;
  cursor: pointer;
  z-index: 1;
}
.back-icon {
  width: 12px;
  height: 12px;
  border-left: 3px solid #0a84ff;
  border-bottom: 3px solid #0a84ff;
  transform: rotate(45deg);
  margin-left: 20px;
  margin-right: 2px;
  border-radius: 3px;
  cursor: pointer;
  z-index: 1;
}
.current {
  text-align: center;
  position: absolute;
  width: 100%;
  font-weight: bold;
  color: #1c1c1e;
}
</style>