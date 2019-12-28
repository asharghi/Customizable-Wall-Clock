<template>
  <div class="container">
    <div class="empty-settings-row"></div>
    <BoolSetting
      label="Aktiver endringer"
      :modelValue="localSettings.active"
      @change="updateValue('active', $event)"
    />
    <div class="empty-settings-row"></div>
    <SliderSetting
      label="Luft fra kanten"
      :min="0"
      :max="50"
      :modelValue="localSettings.space"
      :disabled="!localSettings.active"
      @change="updateValue('space', parseInt($event))"
    />
    <SliderSetting
      label="Bredde"
      :min="0"
      :max="25"
      :modelValue="localSettings.width"
      :disabled="!localSettings.active"
      @change="updateValue('width', parseInt($event))"
    />
    <SliderSetting
      label="Høyde"
      :min="0"
      :max="25"
      :modelValue="localSettings.height"
      :disabled="!localSettings.active"
      @change="updateValue('height', parseInt($event))"
    />
    <SliderSetting
      label="Radius"
      :min="0"
      :max="20"
      :modelValue="localSettings.radius"
      :disabled="!localSettings.active"
      @change="updateValue('radius', parseInt($event))"
    />
    <div class="empty-settings-row"></div>
    <SliderSetting
      label="Farge"
      :min="0"
      :max="360"
      :modelValue="localSettings.hue"
      :hideNumberValue="true"
      :disabled="!localSettings.active"
      @change="updateValue('hue', parseInt($event))"
      sliderClass="color-picker"
    />
    <SliderSetting
      label="Lysstyrke"
      :min="0"
      :step="5"
      :max="100"
      :modelValue="localSettings.luminosity"
      :hideNumberValue="true"
      :disabled="!localSettings.active"
      @change="updateValue('luminosity', parseInt($event))"
      sliderClass="darkness-picker"
    />
  </div>
</template>

<script>
import SliderSetting from "./SliderSetting";
import BoolSetting from "./BoolSetting";
export default {
  components: {
    SliderSetting,
    BoolSetting
  },
  props: {
    settings: {
      type: Object,
      default: () => {
        return {
          space: 20,
          width: 5,
          height: 5,
          radius: 5,
          hue: 50,
          luminosity: 50,
          active: false
        };
      }
    }
  },
  data() {
    return {
      localSettings: this.settings
    };
  },
  methods: {
    updateValue(dataPropertyName, value) {
      this.localSettings[dataPropertyName] = value;
      this.$emit("change", this.localSettings);
    }
  },
  watch: {
    settings: {
      handler(newValue) {
        this.localSettings = newValue;
      },
      deep: true
    }
  }
};
</script>
<style scoped>
.container {
  display: inline-block;
  width: 100%;
  position: absolute;
}
</style>