<template>
  <div>
    {{ text }}
  </div>
</template>

<script>

export default {
  name: 'MapPopupContent',
  props: {
    type: {
      type: String,
      default: ''
    },
    text: {
      type: String,
      default: ''
    }
  }
};
</script>
