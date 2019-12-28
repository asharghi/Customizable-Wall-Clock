<template>
  <div class="settings-row">
    <div class="settings-label">
      {{label}}
    </div>
    <div>
      <label class="switch">
      <input type="checkbox" v-model="localValue" @input="$emit('change', !localValue)"/>
      <span class="slider"></span>
    </label>
    </div>
    
  </div>
</template>

<script>
export default {
  props: {
    modelValue: {
      type: Boolean,
      default: false
    },
    label: {
      type: String,
      default: ''
    }
  },
  data() {
    return {
      localValue: this.modelValue
    };
  },
  watch: {
    modelValue: function(newValue, oldValue) {
      this.localValue = newValue;
    }
  }
};
</script>
<style scoped>
.settings-row {
  padding: 10px 20px 10px 20px;
  background: #ffffff;
  border-bottom: 1px solid #d8d8d8;
  display: flex;
  align-items: center;
  justify-content: space-between;
}

.settings-label {
  font-family: sans-serif;
  color: #5c5c5c;
  font-weight: 100;
  font-size: 14px;
}

.switch {
  display: inline-block;
  position: relative;
  width: 60px;
  height: 34px;
}

.switch input {
  opacity: 0;
  width: 0;
  height: 0;
}

.slider {
  position: absolute;
  cursor: pointer;
  top: 0;
  left: 0;
  right: 0;
  bottom: 0;
  background-color: #ccc;
  -webkit-transition: 0.4s;
  transition: 0.4s;
  border-radius: 34px;
}

.slider:before {
  position: absolute;
  content: "";
  height: 26px;
  width: 26px;
  left: 4px;
  bottom: 4px;
  background-color: white;
  -webkit-transition: 0.4s;
  transition: 0.4s;
  border-radius: 50%;
}

input:checked + .slider {
  background-color: #2ecc71;
}

input:checked + .slider:before {
  -webkit-transform: translateX(26px);
  -ms-transform: translateX(26px);
  transform: translateX(26px);
}
</style>